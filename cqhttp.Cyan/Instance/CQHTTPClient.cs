using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall;
using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.Instance {
    /// <summary>以HTTP协议调用API</summary>
    public class CQHTTPClient : CQApiClient {
        /// <summary></summary>
        public CQHTTPClient (string accessUrl, string accessToken = ""):
            base (accessUrl, accessToken) {

            }
        /// <summary>发送API请求</summary>
        public override async Task<ApiResponse> SendRequestAsync (ApiRequest x) {
            return await HTTPApiSender.PostJsonAsync (
                dest: accessUrl + x.apiPath,
                param: x.content,
                apiToken: accessToken
            );
        }
    }
}