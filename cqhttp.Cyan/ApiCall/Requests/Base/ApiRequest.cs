using cqhttp.Cyan.ApiCall.Results.Base;
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
        [JsonIgnore]
        public string apiPath { get; protected set; }
        /// <summary></summary>
        [JsonIgnore]
        public ApiResult response;
        /// <summary></summary>
        public ApiRequest (string r) => apiPath = r;
        /// <summary></summary>
        [JsonIgnore]
        virtual public string content {
            get { return JsonConvert.SerializeObject (this); }
        }
    }
}