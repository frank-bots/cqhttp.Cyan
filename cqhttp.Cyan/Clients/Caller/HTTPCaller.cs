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
        HttpClient client = new HttpClient ();
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
            client.Timeout = new System.TimeSpan (0, 0, Config.timeout);
        }
        public async Task<ApiResult> SendRequestAsync (ApiRequest request) {
            return await PostJsonAsync (accessUrl, request, accessToken);
        }
        async Task<ApiResult> PostJsonAsync (string host, ApiRequest request, string access_token = "") {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpContent content = new StringContent (
                request.content,
                Encoding.UTF8, "application/json"
            )) {
                if (string.IsNullOrEmpty (access_token) == false)
                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue ("Token", access_token);
                try {
                    for (int i = 0; i < Config.network_max_failure && (i == 0 ||
                            response.IsSuccessStatusCode == false); i++) {
                        response = await client.PostAsync (host + request.api_path, content);
                    }
                } catch (HttpRequestException) {
                    Log.Error ("HTTP API连接错误");
                    throw new Exceptions.NetworkFailureException ("您有没有忘记插网线emmmmmm?");
                }
                if (response.IsSuccessStatusCode == false) {
                    Log.Error (
                        $"调用HTTP API{request.content}得到了错误的返回值{response.StatusCode}"
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