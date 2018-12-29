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
        public string apiPath { get; protected set; }
        /// <summary></summary>
        public ApiResult response;
        /// <summary></summary>
        public ApiRequest (string r) => apiPath = r;
        /// <summary></summary>
        virtual public string content {
            get { throw new Exceptions.NullApicallException (); }
        }
    }
}