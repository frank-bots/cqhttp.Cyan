using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Clients {
    /// <summary></summary>
    public partial class CQApiClient {
        /// <summary>调用相应API</summary>
        public async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            var result = await caller.SendRequestAsync (x);
            Log.Info ($"进行了{x.GetType ()}请求");
            if (group_table != null) {
                if (x is GetGroupMemberInfoRequest) {
                    var r = x.response as GetGroupMemberInfoResult;
                    group_table[r.memberInfo.group_id].TryAdd (
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
    }
}