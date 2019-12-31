using cqhttp.Cyan.ApiCall.Results.Base;
using Newtonsoft.Json;

/*

*/
namespace cqhttp.Cyan.ApiCall.Requests.Base {
    /// <summary>
    /// 调用API的请求
    /// </summary>
    public abstract class ApiRequest {
        /// <summary></summary>
        [JsonIgnore]
        public string api_path { get; protected set; }
        /// <summary></summary>
        [JsonIgnore]
        public ApiResult response;
        /// <summary></summary>
        public ApiRequest (string r) => api_path = r;
        /// <summary></summary>
        [JsonIgnore]
        public string content {
            get { return JsonConvert.SerializeObject (this); }
        }
    }
}