using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace cqhttp.Cyan.Api {
    /// <summary>
    /// 在底层调用API
    /// </summary>
    public class ApiCaller {
        public async static Task<ApiResponse> Get (string dest, params (string, string) [] param) {
            if (param.Length > 0) dest += "?";
            foreach (var i in param)
                dest += i.Item1 + "=" + i.Item2;
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpClient httpClient = new HttpClient ()) {
                httpClient.Timeout = new System.TimeSpan (0, 0, Config.timeOut);
                if (string.IsNullOrEmpty (Config.apiToken) == false)
                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue ("Token", Config.apiToken);
                try {
                    for (int i = 0; i < Config.networkMaxFailure && (i == 0 ||
                            response.IsSuccessStatusCode == false); i++) {
                        response = await httpClient.GetAsync (dest);
                    }
                } catch (HttpRequestException) {
                    //logger.log
                    throw new NetworkFailureException ("您有没有忘记插网线emmmmmm?");
                }
                if (response.IsSuccessStatusCode == false) {
                    //logger.log
                    throw new NetworkFailureException ($"GET调用api出错,HTTP code{response.StatusCode}");
                }
                //try {
                return new ApiResponse (await response.Content.ReadAsStringAsync ());
                //} catch { }
            }
        }
        // may it never be used
        public async static Task<ApiResponse> PostForm (string dest, params KeyValuePair<string, string>[] param) {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpClient httpClient = new HttpClient ())
            using (HttpContent content = new FormUrlEncodedContent (param)) {
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
            return new ApiResponse(await response.Content.ReadAsStringAsync());
        }
        public async static Task<ApiResponse> PostJson (string dest, object param) {
            HttpResponseMessage response = new HttpResponseMessage ();
            using (HttpContent content = new StringContent (
                Newtonsoft.Json.JsonConvert.SerializeObject (param),
                Encoding.UTF8, "application/json"))
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
            //try{
            return new ApiResponse (await response.Content.ReadAsStringAsync ());
            //}catch{}
        }
    }
}