using cqhttp.Cyan.ApiCall.Results;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary></summary>
    [Newtonsoft.Json.JsonObject]
    public class GetLoginInfoRequest : Base.ApiRequest {
        /// <summary></summary>
        public GetLoginInfoRequest () : base ("/get_login_info") {
            this.response = new GetLoginInfoResult ();
        }
    }
}