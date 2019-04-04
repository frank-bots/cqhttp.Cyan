using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary></summary>
    [JsonObject]
    public class SetGroupCardRequest : RateLimitableRequest {
        [JsonProperty] string card;
        [JsonProperty] long group_id;
        [JsonProperty] long user_id;
        /// <param name="group_id"></param>
        /// <param name="user_id"></param>
        /// <param name="card">设置的群名片</param>
        /// <param name="isRateLimited">是否为限速调用</param>
        public SetGroupCardRequest (
                long group_id, long user_id,
                string card, bool isRateLimited = false):
            base ("/set_group_card", isRateLimited) {
                this.response = new Results.EmptyResult ();
                this.card = card;
                this.group_id = group_id;
                this.user_id = user_id;
            }
    }
}