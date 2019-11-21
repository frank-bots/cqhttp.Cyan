using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 群组匿名用户禁言
    /// </summary>
    [JsonObject]
    public class SetGroupAnonymousBanRequest : ApiRequest {
        [JsonProperty] long group_id;
        [JsonProperty] string flag;
        [JsonProperty] long duration;
        ///
        public SetGroupAnonymousBanRequest (long group_id, string flag, long duration = 1800) : base ("/set_group_anonymous_ban") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.flag = flag;
            this.duration = duration;
        }
    }
}