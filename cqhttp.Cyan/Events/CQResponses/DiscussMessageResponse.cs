using cqhttp.Cyan.Messages;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQResponses {
    /// <summary>
    /// 讨论组消息回复
    /// </summary>
    [JsonObject]
    public class DiscussMessageResponse : Base.CQReplyMessageResponse {
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty] public bool at_sender { get; private set; }
        /// <param name="reply">回复消息</param>
        /// <param name="auto_escape">是否自动转义字符串</param>
        /// <param name="at_sender">是否@发送者</param>
        public DiscussMessageResponse (Message reply, bool auto_escape, bool at_sender = true):
            base (reply, auto_escape) { this.at_sender = at_sender; }
    }
}