using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;
using cqhttp.Cyan.Events.EventListener;
using cqhttp.Cyan.Events.MetaEvents;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Instance {
    /// <summary></summary>
    public abstract class CQApiClient {
        /// <summary>
        /// 酷Q配置中的access_token
        /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E8%AF%B7%E6%B1%82%E6%96%B9%E5%BC%8F</see>
        /// </summary>
        public string accessToken = "";
        /// <summary>
        /// API地址
        /// </summary>
        public string accessUrl;
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

        /// <summary>
        /// 指向本实例的群组记录对象
        /// </summary>
        public Utils.GroupTable groupTable = null;
        /// <summary>
        /// 消息记录
        /// </summary>
        public Utils.MessageTable messageTable = null;

        /// <summary></summary>
        public CQApiClient (
            string accessUrl,
            string accessToken = "",
            bool use_group_table = false,
            bool use_message_table = false
        ) {
            this.accessToken = accessToken;
            this.accessUrl = accessUrl;
            if (!Initiate ().Result) throw new Exceptions.ErrorApicallException ();
            Logger.Log (Verbosity.INFO, $"成功连接到{accessUrl}");
            if (use_group_table) this.groupTable = new Utils.GroupTable ();
            if (use_message_table) this.messageTable = new Utils.MessageTable ();
        }
        private async Task<bool> Initiate () {
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
            Logger.Log (Verbosity.INFO, $"进行了{x.GetType()}请求");
            if (groupTable != null) {
                if (x is GetGroupListRequest) {
                    foreach (var i in (x.response as GetGroupListResult).groupList) {
                        groupTable[i.Item1].group_name = i.Item2;
                    }
                } else if (x is GetGroupMemberInfoRequest) {
                    var r = x.response as GetGroupMemberInfoResult;
                    groupTable[r.memberInfo.group_id][r.memberInfo.user_id] = r.memberInfo;
                } else if (x is GetGroupMemberListRequest) {
                    foreach (var i in (x.response as GetGroupMemberListResult).memberInfo) {
                        groupTable[i.group_id][i.user_id] = i;
                    }
                }
            }
            if (messageTable != null) {
                if (x.response is SendmsgResult) {
                    var i = x.response as SendmsgResult;
                    messageTable.Log (i.message_id, (x as SendmsgRequest).message);
                }
            }
        }
        /// <summary>通用发送请求函数，一般不需调用</summary>
        public virtual Task<ApiResult> SendRequestAsync (ApiRequest x) {
            throw new Exceptions.ErrorApicallException ("未指定Client类型(HTTP/WebSocket)");
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
        protected CQEventListener __eventListener;
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
        protected CQResponse __HandleEvent (CQEvent event_) {
            Logger.Log (Verbosity.DEBUG, $"收到了完整的上报事件{event_.postType}");
            if (event_ is MetaEvent) {
                if (event_ is HeartbeatEvent) {
                    if ((event_ as HeartbeatEvent).status.online)
                        alive = true;
                    else alive = false;
                } else if (event_ is LifecycleEvent) {
                    if ((event_ as LifecycleEvent).enabled)
                        alive = true;
                    else alive = false;
                }
                return new Events.CQResponses.EmptyResponse ();
            } else if (event_ is MessageEvent) {
                if (messageTable != null)
                    messageTable.Log (
                        (event_ as MessageEvent).message_id,
                        (event_ as MessageEvent).message
                    );
                if (Utils.DialoguePool.Handle (this, event_ as MessageEvent))
                    return new Events.CQResponses.EmptyResponse ();
            }

            OnEventAsync?.Invoke (this, event_);
            if (OnEvent != null)
                return OnEvent (this, event_);
            else
                return new Events.CQResponses.EmptyResponse ();
        }
    }
}