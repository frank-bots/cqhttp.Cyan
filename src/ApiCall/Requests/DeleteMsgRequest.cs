using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 撤回消息
    /// </summary>
    public class DeleteMsgRequest : ApiRequest {
        int message_id;
        /// <summary></summary>
        public DeleteMsgRequest (int message_id) : base ("/delete_msg") {
            this.response = new Results.EmptyResult();
            this.message_id = message_id;
        }
        /// <summary></summary>
        public override string content {
            get {
                return $"{{\"message_id\":{message_id}}}";
            }
        }
    }
}