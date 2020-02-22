using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Events.CQResponses.Base;
using cqhttp.Cyan.Events.MetaEvents;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Clients {
    /// <summary></summary>
    public partial class CQApiClient {
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
        async Task ProcessMessageEvent (MessageEvent message) {
            switch (message) {
            case GroupMessageEvent group_message:
                var group_id = group_message.group_id;
                if (
                    group_table == null ||
                    !group_table[group_id].ContainsKey (self_id)
                ) {
                    try {
                        var info = await SendRequestAsync (
                            new GetGroupMemberInfoRequest (
                                group_id,
                                this.self_id,
                                no_cache: true
                            )
                        ) as GetGroupMemberInfoResult;
                        if (group_table != null)
                            group_table[group_id][self_id] = info.memberInfo;
                        group_message.self_info = info.memberInfo;
                    } catch { }
                } else {
                    group_message.self_info
                        = group_table[group_id][self_id];
                }
                break;
            case PrivateMessageEvent private_message:
                break;
            case DiscussMessageEvent discuss_message:
                break;
            }
        }
        /// <summary></summary>
        protected async Task<CQResponse> HandleEvent (CQEvent e) {
            Log.Debug ($"收到了完整的上报事件{e.postType}");
            switch (e) {
            case HeartbeatEvent heartbeat:
                if (heartbeat.status.online) {
                    alive = true;
                    alive_counter++;
                    var task = Task.Run (() => {
                        System.Threading.Thread.Sleep (
                            (int) heartbeat.interval
                        );
                        if (alive_counter-- == 0)
                            alive = false;
                    });
                } else alive = false;
                return new EmptyResponse ();
            case LifecycleEvent lifecycle:
                alive = lifecycle.enabled ? true : false;
                return new EmptyResponse ();
            case MessageEvent message:
                alive = true;
                if (message_table != null)
                    message_table.Log (
                        message.message_id,
                        message.message
                    );
                await ProcessMessageEvent (message);
                if (DialoguePool.Handle (this, (e as MessageEvent)))
                    return new EmptyResponse ();
                break;
            }
            try {
                if (OnEventAsync != null)
                    await OnEventAsync (this, e);
                if (OnEvent != null)
                    return OnEvent (this, e);
                else
                    return new EmptyResponse ();
            } catch (InvokeDialogueException d) {
                DialoguePool.Join ((e as MessageEvent).GetEndpoint (), d.content);
                return new EmptyResponse ();
            }
        }
    }
}