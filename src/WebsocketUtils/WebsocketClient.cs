using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.WebsocketUtils {
    /// <summary>
    /// As Websocket Client
    /// </summary>
    static class ConnectionPool {
        private static Dictionary<string, ClientWebSocket> pool =
            new Dictionary<string, ClientWebSocket> ();
        private static Dictionary<string, string> result =
            new Dictionary<string, string> ();
        private static Dictionary<string, object> resultLock =
            new Dictionary<string, object> ();
        private static Dictionary<string, Func<Task>> receive =
            new Dictionary<string, Func<Task>> ();
        private static async Task Connect (string uri) {
            pool[uri] = new ClientWebSocket ();
            await pool[uri].ConnectAsync (
                new System.Uri (uri),
                new CancellationToken ()
            );
            receive[uri] = async () => {
                byte[] buffer = new byte[1024 * 3];
                result[uri] = "";
                if (resultLock.ContainsKey (uri) == false) {
                    resultLock.Add (uri, new object ());
                }
                while (true) {
                    var res = await pool[uri].ReceiveAsync (
                        buffer: buffer,
                        cancellationToken: new CancellationToken ()
                    );
                    lock (resultLock[uri]) {
                        result[uri] += Encoding.UTF8.GetString (buffer).TrimEnd ('\0');
                    }
                    if (res.EndOfMessage)
                        break;
                }
            };
        }

        private static async Task EnsureConnected (string uri) {
            if (pool.ContainsKey (uri)) {
                await Config.TimeOut (
                    () => pool[uri].State != WebSocketState.Connecting,
                    "websocket 连接超时"
                );
                if (pool[uri].State == WebSocketState.Open)
                    return;
                pool[uri].Abort ();
                pool[uri].Dispose ();
            }
            await Connect (uri);
        }
        /// <summary>
        /// 不要调用
        /// </summary>
        public static async Task<int> SendJsonAsync (string uri, JObject obj) {
            await EnsureConnected (uri);
            int time = System.DateTime.Now.Millisecond;
            obj["echo"] = time;
            try {
                await pool[uri].SendAsync (
                    buffer: Encoding.UTF8.GetBytes (obj.ToString (Formatting.None)),
                    messageType: WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: new CancellationToken () //not going to cancel
                );
            } catch (System.Exception e) {
                Logger.Error ("Websocket调用API失败");
                Logger.Debug ("调用:" + obj.ToString ());
                Logger.Debug ("Traceback:" + e.StackTrace);
                throw new Exceptions.NetworkFailureException ("调用API失败");
            }
            return time;
        }
        public static async Task<string> GetResponse (string uri) {
            await receive[uri] ();
            return result[uri];
        }
        public static async Task CloseAsync (string uri) {
            await pool[uri].CloseAsync (
                WebSocketCloseStatus.NormalClosure,
                "", new CancellationToken ()
            );
        }
    }
}