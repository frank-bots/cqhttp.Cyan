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
        private static Dictionary<int, string> result =
            new Dictionary<int, string> ();
        private static Dictionary<string, bool> state =
            new Dictionary<string, bool> ();
        private static Dictionary<string, Queue<string>> send_queue =
            new Dictionary<string, Queue<string>> ();
        static SemaphoreSlim lock_ = new SemaphoreSlim (1, 1);
        private static async Task Connect (string uri) {
            pool[uri] = new ClientWebSocket ();
            state[uri] = false;
            send_queue[uri] = new Queue<string> ();
            await pool[uri].ConnectAsync (
                new System.Uri (uri),
                new CancellationToken ()
            );
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
        public static async Task < (int, string) > SendJsonAsync (string uri, JObject obj) {
            await lock_.WaitAsync ();
            try {
                string result = "";
                int time = System.DateTime.Now.Millisecond;
                await EnsureConnected (uri);
                obj["echo"] = time;
                await pool[uri].SendAsync (
                    buffer: Encoding.UTF8.GetBytes (obj.ToString (Formatting.None)),
                    messageType: WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: new CancellationToken () //not going to cancel
                );
                byte[] buffer = new byte[1024 * 3];
                while (true) {
                    var res = await pool[uri].ReceiveAsync (
                        buffer: buffer,
                        cancellationToken: new CancellationToken ()
                    );
                    result += Encoding.UTF8.GetString (buffer).TrimEnd ('\0');

                    if (res.EndOfMessage) {
                        ConnectionPool.result[time] = result;
                        break;
                    }
                }
                return (time, result);
            } catch (System.Exception e) {
                Logger.Error ("Websocket调用API失败");
                Logger.Debug ("调用:" + obj.ToString ());
                Logger.Debug ("Traceback:" + e.StackTrace);
                throw new Exceptions.NetworkFailureException ("调用API失败");
            } finally {
                lock_.Release ();
            }
        }
        public static async Task CloseAsync (string uri) {
            await pool[uri].CloseAsync (
                WebSocketCloseStatus.NormalClosure,
                "", new CancellationToken ()
            );
        }
    }
}