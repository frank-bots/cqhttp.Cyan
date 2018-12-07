using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.Api.Requests.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Api {
    /// <summary>
    /// 在底层调用API
    /// </summary>
    public class ApiSender {
        public async static Task<ApiResponse> Post (ApiRequest request) {
            return await PostJson ("http://service.std-frank.club:233"+request.apiUrl, request.content);
        }
        private async static Task<ApiResponse> PostJson (string dest, string param) {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpContent content = new StringContent (
                param, Encoding.UTF8, "application/json"))
            using (HttpClient httpClient = new HttpClient ()) {
                httpClient.Timeout = new System.TimeSpan (0, 0, Config.timeOut);
                if (string.IsNullOrEmpty (Config.apiToken) == false)
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue ("Token", Config.apiToken);
                try {
                    for (int i = 0; i < Config.networkMaxFailure && (i == 0 ||
                            response.IsSuccessStatusCode == false); i++) {
                        response = await httpClient.PostAsync (dest, content);
                    }
                } catch (HttpRequestException) {
                    //logger.log
                    throw new NetworkFailureException ("您有没有忘记插网线emmmmmm?");
                }
                if (response.IsSuccessStatusCode == false) {
                    //logger.log
                    throw new NetworkFailureException ($"POST调用api出错,HTTP code{response.StatusCode}");
                }
            }
            try {
                return
                JToken.Parse (
                    await response.Content.ReadAsStringAsync ()
                ).ToObject<ApiResponse> ();
            } catch (JsonException) {
                throw new ErrorApicallException ($"调用api{dest}时返回值无法反序列化");
            }
        }
    }
}