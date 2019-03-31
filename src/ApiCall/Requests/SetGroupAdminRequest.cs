using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 群组设置管理员
    /// </summary>
    [JsonObject]
    public class SetGroupAdminRequest : ApiRequest {
        [JsonProperty ("group_id")] long group_id;
        [JsonProperty ("user_id")] long user_id;
        [JsonProperty ("enable")] bool enable;
        ///
        public SetGroupAdminRequest (long group_id, long user_id, bool enable) : base ("/set_group_admin") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.user_id = user_id;
            this.enable = enable;
        }
    }
}