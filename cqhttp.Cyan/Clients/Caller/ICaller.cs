using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;

namespace cqhttp.Cyan.Clients.Callers {
    /// <summary>
    /// 
    /// </summary>
    public interface ICaller {
        /// <summary>
        /// 调用API
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns></returns>
        Task<ApiResult> SendRequestAsync (ApiRequest request);
    }
}