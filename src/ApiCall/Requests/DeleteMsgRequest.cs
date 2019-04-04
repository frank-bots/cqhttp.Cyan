using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 撤回消息
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class DeleteMsgRequest : ApiRequest {
        [Newtonsoft.Json.JsonProperty] int message_id;
        /// <summary></summary>
        public DeleteMsgRequest (int message_id) : base ("/delete_msg") {
            this.response = new Results.EmptyResult ();
            this.message_id = message_id;
        }
    }
}