using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiHTTP.Requests.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiHTTP {
    /// <summary>
    /// 在底层调用API
    /// </summary>
    public class ApiSender {
        public async static Task<ApiResponse> DebugPostAsync (ApiRequest request) {
            return await PostJsonAsync ("http://service.std-frank.club:233" + request.apiUrl, request.content);
        }
        public async static Task<ApiResponse> PostJsonAsync (string dest, string param, string apiToken = "") {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpContent content = new StringContent (
                param, Encoding.UTF8, "application/json"))
            using (HttpClient httpClient = new HttpClient ()) {
                httpClient.Timeout = new System.TimeSpan (0, 0, Config.timeOut);
                if (string.IsNullOrEmpty (apiToken) == false)
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue ("Token", apiToken);
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