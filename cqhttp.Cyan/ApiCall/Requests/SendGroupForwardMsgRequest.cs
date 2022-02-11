using System.Collections.Generic;
using System.Linq;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>发送转发多条消息的专用Api，仅用于go-cqhttp</summary>
    [Newtonsoft.Json.JsonObject]
    public class SendGroupForwardMsgRequest : RateLimitableRequest {
        ///
        public long group_id;
        ///
        public List<ElementForward.ElementNode> messages;
        /// <summary></summary>
        public SendGroupForwardMsgRequest (
            long group_id, IEnumerable<ElementForward.ElementNode> msgs, bool isRateLimited = false
        ) : base ("/send_group_forward_msg", isRateLimited) {
            this.response = new EmptyResult ();
            this.messages = msgs.ToList ();
            this.group_id = group_id;
        }
    }
}
