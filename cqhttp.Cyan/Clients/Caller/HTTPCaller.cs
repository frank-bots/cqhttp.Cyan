using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;

namespace cqhttp.Cyan.Clients.Callers {
    class HTTPCaller : ICaller {
        string accessUrl;
        string accessToken = "";
        /// <summary>
        /// 使用HTTP方式调用API
        /// </summary>
        /// <param name="accessUrl"></param>
        /// <param name="accessToken">
        /// 酷Q配置中的access_token
        /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E8%AF%B7%E6%B1%82%E6%96%B9%E5%BC%8F</see>
        /// </param>
        /// <returns></returns>
        public HTTPCaller (
            string accessUrl,
            string accessToken = ""
        ) {
            this.accessUrl = accessUrl;
            this.accessToken = accessToken;
        }
        public async Task<ApiResult> SendRequestAsync (ApiRequest request) {
            return await PostJsonAsync (accessUrl, request, accessToken);
        }
        async static Task<ApiResult> PostJsonAsync (string host, ApiRequest request, string accessToken = "") {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpContent content = new StringContent (
                request.content,
                Encoding.UTF8, "application/json"
            ))
            using (HttpClient httpClient = new HttpClient ()) {
                httpClient.Timeout = new System.TimeSpan (0, 0, Config.timeOut);
                if (string.IsNullOrEmpty (accessToken) == false)
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue ("Token", accessToken);
                try {
                    for (int i = 0; i < Config.networkMaxFailure && (i == 0 ||
                            response.IsSuccessStatusCode == false); i++) {
                        response = await httpClient.PostAsync (host + request.apiPath, content);
                    }
                } catch (HttpRequestException) {
                    Logger.Error ("HTTP API连接错误");
                    throw new Exceptions.NetworkFailureException ("您有没有忘记插网线emmmmmm?");
                }
                if (response.IsSuccessStatusCode == false) {
                    Logger.Error (
                        $"调用HTTP API{await content.ReadAsStringAsync()}得到了错误的返回值{response.StatusCode}"
                    );
                    throw new Exceptions.NetworkFailureException ($"POST调用api出错");
                }
            }
            request.response.Parse (
                Newtonsoft.Json.Linq.JToken.Parse (
                    await response.Content.ReadAsStringAsync ()
                )
            );
            return request.response;
        }
    }
}