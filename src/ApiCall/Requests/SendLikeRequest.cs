using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 给好友的名片点个赞👍
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class SendLikeRequest : RateLimitableRequest {
        [Newtonsoft.Json.JsonProperty] long user_id;
        [Newtonsoft.Json.JsonProperty] int times;
        ///
        public SendLikeRequest (long user_id, int times, bool limit_rate = false) : base ("/send_like", limit_rate) {
            this.response = new Results.EmptyResult ();
            this.user_id = user_id;
            this.times = times;
        }
    }
}