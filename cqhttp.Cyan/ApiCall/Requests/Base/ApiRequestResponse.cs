using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*

*/
namespace cqhttp.Cyan.ApiCall.Requests.Base {
    /// <summary>
    /// 调用API的请求
    /// </summary>
    public abstract class ApiRequest {
        /// <summary></summary>
        public string apiPath { get; private set; }
        /// <summary></summary>
        public ApiResponse response;
        /// <summary></summary>
        public ApiRequest () {
            throw new NullApicallException ();
        }
        /// <summary></summary>
        public ApiRequest (string r) => apiPath = r;
        /// <summary></summary>
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