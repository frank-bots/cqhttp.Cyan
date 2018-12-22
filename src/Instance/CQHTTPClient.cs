using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Result.Base;
using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Instance {
    /// <summary>以HTTP协议调用API</summary>
    public class CQHTTPClient : CQApiClient {
        /// <summary></summary>
        public CQHTTPClient (string accessUrl, string accessToken = "", int listen_port = -1, string secret = ""):
            base (accessUrl, accessToken) {
                if (listen_port != -1) {
                    this.__eventListener = new Events.EventListener.HttpEventListener (listen_port, secret);
                    this.__eventListener.StartListen (__HandleEvent);
                }
            }
        /// <summary>发送API请求</summary>
        public override async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            return await PostJsonAsync (
                accessUrl, x, accessToken
            );
        }
        private async static Task<ApiResult> PostJsonAsync (string host, ApiRequest request, string apiToken = "") {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpContent content = new StringContent (
                request.content, Encoding.UTF8, "application/json"))
            using (HttpClient httpClient = new HttpClient ()) {
                httpClient.Timeout = new System.TimeSpan (0, 0, Config.timeOut);
                if (string.IsNullOrEmpty (apiToken) == false)
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue ("Token", apiToken);
                try {
                    for (int i = 0; i < Config.networkMaxFailure && (i == 0 ||
                            response.IsSuccessStatusCode == false); i++) {
                        response = await httpClient.PostAsync (host + request.apiPath, content);
                    }
                } catch (HttpRequestException) {
                    Logger.Log (
                        Verbosity.ERROR,
                        "HTTP API连接错误"
                    );
                    throw new Exceptions.NetworkFailureException ("您有没有忘记插网线emmmmmm?");
                }
                if (response.IsSuccessStatusCode == false) {
                    Logger.Log (
                        Verbosity.ERROR,
                        $"调用HTTP API{await content.ReadAsStringAsync()}得到了错误的返回值{response.StatusCode}"
                    );
                    throw new Exceptions.NetworkFailureException ($"POST调用api出错");
                }
            }
            request.response.Parse (
                await response.Content.ReadAsStringAsync ()
            );
            return request.response;
        }
    }

}