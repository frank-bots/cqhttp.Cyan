using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 全 员 🈲️ 💬
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class SetGroupWholeBanRequest : ApiRequest {
        [Newtonsoft.Json.JsonProperty] long group_id;
        [Newtonsoft.Json.JsonProperty] bool enable;
        /// <param name="group_id">要禁言的群号</param>
        /// <param name="enable">是否禁言</param>
        public SetGroupWholeBanRequest (long group_id, bool enable = true) : base ("/set_group_whole_ban") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.enable = enable;
        }
    }
}