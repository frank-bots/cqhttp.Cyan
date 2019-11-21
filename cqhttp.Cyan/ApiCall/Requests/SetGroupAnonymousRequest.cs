using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 设置群组能否匿名聊天
    /// </summary>
    [JsonObject]
    public class SetGroupAnonymousRequest : ApiRequest {
        [JsonProperty] long group_id;
        [JsonProperty] bool enable;
        ///
        public SetGroupAnonymousRequest (long group_id, bool enable = true) : base ("/set_group_anonymous") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.enable = enable;
        }
    }
}