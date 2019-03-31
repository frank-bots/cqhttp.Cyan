using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {

    /// <summary>
    /// 响应事件
    /// </summary>
    [JsonObject]
    public class HandleQuickOperationRequest : Base.ApiRequest {
        [JsonProperty ("context")]
        string context;
        [JsonProperty ("operation")]
        string operation;
        ///
        public HandleQuickOperationRequest (string context, string operation):
            base ("/.handle_quick_operation") {
                this.response = new Results.EmptyResult ();
                this.context = context;
                this.operation = operation;
            }
    }
}