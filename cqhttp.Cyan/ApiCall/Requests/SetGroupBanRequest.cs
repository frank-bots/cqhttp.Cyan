using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 堵上一个用户的嘴
    /// </summary>
    [JsonObject]
    public class SetGroupBanRequest : RateLimitableRequest {
        [JsonProperty] long duration;
        [JsonProperty] long group_id;
        [JsonProperty] long user_id;
        /// <param name="group_id">群号</param>
        /// <param name="user_id">用户QQ</param>
        /// <param name="duration">禁言时长</param>
        /// <param name="is_ratelimited">是否为限速调用</param>
        public SetGroupBanRequest (long group_id, long user_id, long duration = 1800, bool is_ratelimited = false):
            base ("/set_group_ban", is_ratelimited) {
                this.group_id = group_id;
                this.user_id = user_id;
                this.response = new Results.EmptyResult ();
                this.duration = duration;
            }
    }
}