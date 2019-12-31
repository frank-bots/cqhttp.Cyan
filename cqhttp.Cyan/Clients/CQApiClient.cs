using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;
using cqhttp.Cyan.Events.MetaEvents;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Clients {
    /// <summary></summary>
    public class CQApiClient {
        /// <summary>
        /// 当前实例的QQ号
        /// </summary>
        public long self_id { get; private set; }
        /// <summary>
        /// 当前实例的QQ昵称
        /// </summary>
        public string self_nick { get; private set; }
        /// <summary>
        /// 连接到的实例是否为酷Q pro
        /// </summary>
        public bool is_pro { get; private set; }
        /// <summary>
        /// 表示插件是否正常运行
        /// </summary>
        public bool alive { get; private set; }
        int alive_counter = 0;
        /// <summary>
        /// 指向本实例的群组记录对象
        /// </summary>
        public Utils.GroupTable group_table = null;
        /// <summary>
        /// 消息记录
        /// </summary>
        public Utils.MessageTable message_table = null;

        ///
        protected Callers.ICaller caller;
        ///
        protected Listeners.IListener listener;

        /// <summary></summary>
        public CQApiClient (
            Callers.ICaller caller,
            Listeners.IListener listener,
            bool use_group_table = false,
            bool use_message_table = false
        ) {
            this.caller = caller;
            this.listener = listener;
            if (use_group_table) this.group_table = new Utils.GroupTable ();
            if (use_message_table) this.message_table = new Utils.MessageTable ();
        }
        ///
        protected async Task<bool> Initiate () {
            GetLoginInfoResult loginInfo =
                await SendRequestAsync (new GetLoginInfoRequest ())
            as GetLoginInfoResult;
            GetVersionInfoResult versionInfo =
                await SendRequestAsync (new GetVersionInfoRequest ())
            as GetVersionInfoResult;
            this.self_id = loginInfo.user_id;
            this.self_nick = loginInfo.nickname;
            this.is_pro = (versionInfo.instanceVersionInfo.coolq_edition == "pro");
            return true;
        }
        ///
        protected void RequestPreprocess (ApiRequest x) {
            Log.Info ($"进行了{x.GetType()}请求");
            if (group_table != null) {
                if (x is GetGroupMemberInfoRequest) {
                    var r = x.response as GetGroupMemberInfoResult;
                    group_table[r.memberInfo.group_id].Add (
                        r.memberInfo.user_id, r.memberInfo
                    );
                } else if (x is GetGroupMemberListRequest) {
                    foreach (var i in (x.response as GetGroupMemberListResult).memberInfo) {
                        group_table[i.group_id][i.user_id] = i;
                    }
                }
            }
            if (message_table != null) {
                if (x.response is SendmsgResult) {
                    var i = x.response as SendmsgResult;
                    message_table.Log (i.message_id, (x as SendmsgRequest).message);
                }
            }
        }
        /// <summary>通用发送请求函数，一般不需调用</summary>
        public async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            var result = await caller.SendRequestAsync (x);
            RequestPreprocess (x);
            return result;
        }
        /// <summary>发送消息(自行构造)</summary>
        public async Task<SendmsgResult> SendMessageAsync (
            MessageType messageType,
            long target,
            Message message
        ) => await SendRequestAsync (
            new SendmsgRequest (messageType, target, message)
        ) as SendmsgResult;

        /// <summary>发送消息(自行构造)</summary>
        public async Task<SendmsgResult> SendMessageAsync (
            (MessageType, long) target,
            Message message
        ) => await SendRequestAsync (
            new SendmsgRequest (target.Item1, target.Item2, message)
        ) as SendmsgResult;

        /// <summary>发送纯文本消息</summary>
        public async Task<SendmsgResult> SendTextAsync (
            MessageType messageType,
            long target,
            string text
        ) => await SendRequestAsync (
            new SendmsgRequest (
                messageType, target,
                new Message (new Messages.CQElements.ElementText (text))
            )
        ) as SendmsgResult;

        /// <summary>发送纯文本消息</summary>
        public async Task<SendmsgResult> SendTextAsync (
            (MessageType, long) target,
            string text
        ) => await SendRequestAsync (
            new SendmsgRequest (
                target.Item1, target.Item2,
                new Message (new Messages.CQElements.ElementText (text))
            )
        ) as SendmsgResult;

        //////////////////////////////////////////////////////////////////////////////////////

        /// <summary></summary>
        public delegate CQResponse OnEventDelegate (CQApiClient client, CQEvent eventObj);
        ///
        public delegate Task<CQResponse> OnEventDelegateAsync (CQApiClient client, CQEvent eventObj);
        /// <summary></summary>
        public event OnEventDelegate OnEvent;
        /// <summary>
        /// 异步执行命令，忽略返回值
        /// </summary>
        public event OnEventDelegateAsync OnEventAsync;
        /// <summary></summary>
        protected async Task<CQResponse> HandleEvent (CQEvent e) {
            Log.Debug ($"收到了完整的上报事件{e.postType}");
            if (e is MetaEvent) {
                if (e is HeartbeatEvent) {
                    if ((e as HeartbeatEvent).status.online) {
                        alive = true;
                        alive_counter++;
                        var task = Task.Run (() => {
                            System.Threading.Thread.Sleep (
                                (int) (e as HeartbeatEvent).interval
                            );
                            if (alive_counter-- == 0)
                                alive = false;
                        });
                    } else alive = false;
                } else if (e is LifecycleEvent) {
                    if ((e as LifecycleEvent).enabled)
                        alive = true;
                    else alive = false;
                }
                return new Events.CQResponses.EmptyResponse ();
            } else if (e is MessageEvent) {
                alive = true;
                if (message_table != null)
                    message_table.Log (
                        (e as MessageEvent).message_id,
                        (e as MessageEvent).message
                    );
                if (e is GroupMessageEvent) {
                    var group_id = (e as GroupMessageEvent).group_id;
                    if (group_table == null || group_table[group_id].ContainsKey(self_id) == false) {
                        try {
                            var info = await SendRequestAsync (
                                new GetGroupMemberInfoRequest (
                                    group_id,
                                    this.self_id,
                                    no_cache : true
                                )
                            ) as GetGroupMemberInfoResult;
                            if (group_table != null)
                                group_table[group_id][self_id] = info.memberInfo;
                            (e as GroupMessageEvent).self_info = info.memberInfo;
                        } catch { }
                    } else {
                        (e as GroupMessageEvent).self_info
                            = group_table[group_id][self_id];
                    }
                }
                if (Utils.DialoguePool.Handle (this, (e as MessageEvent)))
                    return new Events.CQResponses.EmptyResponse ();
            }
            try {
                if (OnEventAsync != null)
                    await OnEventAsync (this, e);

                if (OnEvent != null)
                    return OnEvent (this, e);
                else
                    return new Events.CQResponses.EmptyResponse ();
            } catch (Utils.InvokeDialogueException d) {
                ComposeDialogue (ref d, ref e);
                return new Events.CQResponses.EmptyResponse ();
            }
        }
        private static object dialoguePoolLock = new object ();
        private static void ComposeDialogue (
            ref Utils.InvokeDialogueException d,
            ref CQEvent e
        ) {
            lock (dialoguePoolLock) {
                Log.Debug ("got a dialogue");
                long uid = (e as MessageEvent).sender.user_id;
                long bid =
                    (e is GroupMessageEvent) ? (e as GroupMessageEvent).group_id :
                    (e is DiscussMessageEvent) ? (e as DiscussMessageEvent).discuss_id :
                    uid;
                if (d.acceptAll && bid != uid)
                    uid = bid;
                Utils.DialoguePool.Join (uid, bid, d.content);
            }
            return;
        }
    }
}