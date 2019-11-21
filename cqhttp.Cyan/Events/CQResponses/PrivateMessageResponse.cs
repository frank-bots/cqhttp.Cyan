namespace cqhttp.Cyan.Events.CQResponses {
    /// <summary></summary>
    [Newtonsoft.Json.JsonObject]
    public class PrivateMessageResponse : Base.CQReplyMessageResponse {
        /// <param name="reply">回复的消息内容</param>
        /// <param name="auto_escape">是否将字符串转义(一般不用)</param>
        public PrivateMessageResponse (Messages.Message reply, bool auto_escape = false):
            base (reply, auto_escape) { }
    }
}