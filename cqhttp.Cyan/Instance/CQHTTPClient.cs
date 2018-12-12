using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Instance {
    /// <summary>以HTTP协议调用API</summary>
    public class CQHTTPClient : CQApiClient {
        /// <summary></summary>
        public CQHTTPClient (string accessUrl, string accessToken = "", int listen_port = -1, string secret = ""):
            base (accessUrl, accessToken) {
                if (listen_port != -1)
                    this.__eventListener = new Events.EventListener.HttpEventListener (listen_port, secret);
                this.__eventListener.StartListen (__HandleEvent);
            }
        /// <summary>发送API请求</summary>
        public override async Task<ApiResponse> SendRequestAsync (ApiRequest x) {
            return await HTTPApiSender.PostJsonAsync (
                accessUrl, x, accessToken
            );
        }
        
    }
}