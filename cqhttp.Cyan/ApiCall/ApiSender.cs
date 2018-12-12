using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall {
    /// <summary>
    /// 调用HTTP API的抽象层
    /// </summary>
    public class HTTPApiSender {
        /// <summary></summary>
        public async static Task<ApiResponse> PostJsonAsync (string host, ApiRequest request, string apiToken = "") {
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
                    //logger.log
                    throw new NetworkFailureException ("您有没有忘记插网线emmmmmm?");
                }
                if (response.IsSuccessStatusCode == false) {
                    //logger.log
                    throw new NetworkFailureException ($"POST调用api出错,HTTP code{response.StatusCode}");
                }
            }
            try {
                request.response = JToken.Parse (
                    await response.Content.ReadAsStringAsync ()
                ).ToObject<ApiResponse> ();
                return request.response;
            } catch (JsonException) {
                throw new ErrorApicallException ($"调用api{request.apiPath}时返回值无法反序列化");
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketApiSender {
        private static Dictionary < string, (Dictionary<int, ApiResponse>, ClientWebSocket) > pool =
            new Dictionary < string, (Dictionary<int, ApiResponse>, ClientWebSocket) > ();
        /// <summary>
        /// 1、若不存在与host的连接，创建之
        /// 
        /// 2、向host发送API请求request
        /// </summary>
        /// <param name="request">待发送的请求</param>
        /// <param name="host">主机地址 (host的一个可能值为 "ws://domain:port")</param>
        /// <param name="apiToken">access_token</param>
        public static async Task<ApiResponse> WSSendJson (string host, ApiRequest request, string apiToken = "") {
            string dest = host + "/api/";
            (Dictionary<int, ApiResponse>, ClientWebSocket) current;
            if (pool.ContainsKey (dest) == false) {
                pool.Add (dest, (
                    new Dictionary<int, ApiResponse> (),
                    new ClientWebSocket ()
                ));
                current = pool[dest];
                await current.Item2.ConnectAsync (new Uri (dest), new CancellationToken ());
            } else current = pool[dest];
            int time = DateTime.Now.Millisecond;
            string constructor =
                $"{{\"action\":\"{request.apiPath.Substring(1)}\","+
                $"\"params\":{request.content},"+
                $"\"echo\":{time}}}";
            ApiResponse i = new ApiResponse ();
            await current.Item2.SendAsync (
                buffer: Encoding.UTF8.GetBytes (constructor),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken: new CancellationToken ()//not going to cancel
            );

            while (current.Item1.ContainsKey (time) == false)
                Thread.Sleep (20);
            i = current.Item1[time];
            current.Item1.Remove (time);
            return i;
        }
        /// <summary>Websocket关闭所有连接</summary>
        public async static void CleanUp () {
            foreach (var i in pool) {
                i.Value.Item1.Clear ();
                await i.Value.Item2.CloseAsync (
                    closeStatus: WebSocketCloseStatus.NormalClosure,
                    statusDescription: "client shutdown",
                    cancellationToken : new CancellationToken ()
                );
            }
        }

    }
}