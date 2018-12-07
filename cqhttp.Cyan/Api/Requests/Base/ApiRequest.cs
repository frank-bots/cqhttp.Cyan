using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*

*/
namespace cqhttp.Cyan.Api.Requests.Base {
    public class ApiRequest {
        public string apiUrl { get; private set; }
        public ApiResponse response;
        public ApiRequest () {
            throw new NullApicallException ();
        }
        public ApiRequest (string r) => apiUrl = r;
        virtual public string content { get { throw new NullApicallException (); } }
    }
    /// <summary>
    /// 调用API时cqhttp的响应
    /// </summary>
    [JsonObject]
    public class ApiResponse {
        /// <summary>
        /// "ok","async","failed"
        /// 参照<see>https://cqhttp.cc/docs/4.6/#/API</see>中有关status的说明
        /// </summary>
        /// <value></value>
        [JsonProperty ("status")]
        public string status { get; private set; } //"ok","async","failed"

        /// <summary>
        /// status ok为0,async为1,failed参照链接
        /// <see>https://d.cqp.me/Pro/%E5%BC%80%E5%8F%91/Error</see>
        /// </summary>
        [JsonProperty ("retcode")]
        public int retcode { get; private set; }

        /// <summary>
        /// 原封不动
        /// <see>https://cqhttp.cc/docs/4.6/#/API</see>
        /// </summary>
        [JsonProperty ("data")]
        public Newtonsoft.Json.Linq.JObject data { get; private set; }
    }
}