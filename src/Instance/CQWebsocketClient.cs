using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.Events.EventListener;
using cqhttp.Cyan.Enums;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Instance {
    /// <summary>websocket协议调用api</summary>
    public class CQWebsocketClient : CQApiClient {
        /// <summary></summary>
        public CQWebsocketClient (string accessUrl, string accessToken = "", string dest_url = "", string secret = ""):
            base (accessUrl, accessToken) {
                if (dest_url.Length != 0) {
                    this.__eventListener = new WebSocketListener (dest_url, secret);
                    this.__eventListener.StartListen (__HandleEvent);
                }
            }
        /// <summary></summary>
        public override async Task<ApiResponse> SendRequestAsync (ApiRequest x) {
                return await WSSendJson (accessUrl, x, accessToken);
            }
            /// <summary></summary>
            ~CQWebsocketClient () {
                CleanUp ();
            }
        private static Dictionary<string, ClientWebSocket> pool =
            new Dictionary<string, ClientWebSocket> ();
        private static async Task<ApiResponse> WSSendJson (string host, ApiRequest request, string apiToken = "") {
            string dest = host + "/api/";
            ClientWebSocket current;
            if (pool.ContainsKey (dest) == false) {
                pool.Add (dest, new ClientWebSocket ());
                current = pool[dest];
                await current.ConnectAsync (new Uri (dest), new CancellationToken ());
            } else current = pool[dest];
            int time = DateTime.Now.Millisecond;
            string constructor =
                $"{{\"action\":\"{request.apiPath.Substring(1)}\","+
                $"\"params\":{request.content},"+
                $"\"echo\":{time}}}";
            Logger.Log(Verbosity.DEBUG,$"向{dest}发送数据{constructor}调用API");
            await current.SendAsync (
                buffer: Encoding.UTF8.GetBytes (constructor),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken: new CancellationToken () //not going to cancel
            );
            byte[] buffer = new byte[1024];
            constructor = "";
            var recvResult = await current.ReceiveAsync (buffer, new CancellationToken ());
            constructor += Encoding.UTF8.GetString (buffer);
            while (recvResult.EndOfMessage == false) {
                recvResult = await current.ReceiveAsync (buffer, new CancellationToken ());
                constructor += Encoding.UTF8.GetString (buffer);
            }
            return JToken.Parse (constructor).ToObject<ApiResponse> ();
        }
        private async static void CleanUp () {
            Logger.Log (Verbosity.INFO, "开始关闭Websocket连接");
            foreach (var i in pool) {
                await i.Value.CloseAsync (
                    closeStatus: WebSocketCloseStatus.NormalClosure,
                    statusDescription: "client shutdown",
                    cancellationToken : new CancellationToken ()
                );
            }
        }
    }
}