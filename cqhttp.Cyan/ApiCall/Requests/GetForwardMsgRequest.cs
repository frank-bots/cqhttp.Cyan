using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 撤回消息
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class GetForwardMsgRequest : ApiRequest {
        [Newtonsoft.Json.JsonProperty] string id;
        /// <summary></summary>
        public GetForwardMsgRequest (string message_id) : base ("/get_forward_msg") {
            this.response = new Results.GetForwardMsgResult ();
            this.id = message_id;
        }
    }
}