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
                return new EmptyResponse ();
            } else if (e is MessageEvent) {
                alive = true;
                if (message_table != null)
                    message_table.Log (
                        (e as MessageEvent).message_id,
                        (e as MessageEvent).message
                    );
                if (e is GroupMessageEvent) {
                    var group_id = (e as GroupMessageEvent).group_id;
                    if (group_table == null || group_table[group_id].ContainsKey (self_id) == false) {
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
                            (e as GroupMessageEvent).self_info = info.memberInfo;
                        } catch { }
                    } else {
                        (e as GroupMessageEvent).self_info
                            = group_table[group_id][self_id];
                    }
                }
                if (DialoguePool.Handle (this, (e as MessageEvent)))
                    return new EmptyResponse ();
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