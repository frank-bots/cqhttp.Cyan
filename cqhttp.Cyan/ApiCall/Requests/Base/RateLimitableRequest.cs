namespace cqhttp.Cyan.ApiCall.Requests.Base {
    /// <summary>
    /// 可限速（异步）调用的API
    /// </summary>
    public class RateLimitableRequest : ApiRequest {
        /// <param name="is_ratelimited">是否进行限速</param>
        /// <param name="r">API url</param>
        public RateLimitableRequest (string r, bool is_ratelimited):
            base (r + (is_ratelimited? "_rate_limited": "")) {

            }
    }
}