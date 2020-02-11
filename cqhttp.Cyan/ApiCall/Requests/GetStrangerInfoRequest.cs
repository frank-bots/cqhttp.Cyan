using cqhttp.Cyan.ApiCall.Results;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary></summary>
    [Newtonsoft.Json.JsonObject]
    public class GetStrangerInfoRequest : Base.ApiRequest {
        [Newtonsoft.Json.JsonProperty] long user_id;
        [Newtonsoft.Json.JsonProperty] bool no_cache;
        /// <summary></summary>
        public GetStrangerInfoRequest (long user_id, bool no_cache = false) : base ("/get_stranger_info") {
            this.user_id = user_id;
            this.no_cache = no_cache;
            this.response = new GetStrangerInfoResult ();
        }
    }
}