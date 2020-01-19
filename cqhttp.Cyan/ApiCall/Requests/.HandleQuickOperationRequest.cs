using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Requests {

    /// <summary>
    /// 响应事件
    /// </summary>
    [JsonObject]
    public class HandleQuickOperationRequest : Base.ApiRequest {
        [JsonProperty] JObject context;
        [JsonProperty] JObject operation;
        ///
        public HandleQuickOperationRequest (string context, string operation):
            base ("/.handle_quick_operation") {
                this.response = new Results.EmptyResult ();
                this.context = JObject.Parse (context);
                this.operation = JObject.Parse (operation);
            }
    }
}