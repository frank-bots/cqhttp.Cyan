using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Fleck;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Instance {
    /// <summary>
    /// As Websocket Client
    /// </summary>
    static class ConnectionPool {
        private static Dictionary<string, ClientWebSocket> pool =
            new Dictionary<string, ClientWebSocket> ();
        static SemaphoreSlim lock_ = new SemaphoreSlim (1, 1);
        private static async Task Connect (string uri) {
            pool[uri] = new ClientWebSocket ();
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
                    buffer: System.Text.Encoding.UTF8.GetBytes (
                        obj.ToString (Newtonsoft.Json.Formatting.None)
                    ),
                    messageType : WebSocketMessageType.Text,
                    endOfMessage : true,
                    cancellationToken : new CancellationToken () //not going to cancel
                );
                byte[] buffer = new byte[1024 * 3];
                while (true) {
                    var res = await pool[uri].ReceiveAsync (
                        buffer: buffer,
                        cancellationToken: new CancellationToken ()
                    );
                    result += System.Text.Encoding.UTF8.GetString (buffer).TrimEnd ('\0');
                    if (res.EndOfMessage)
                        break;
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
    /// <summary>
    /// As Websocket Server
    /// </summary>
    public static class WebsocketDaemon {
        static Dictionary<int, WebSocketServer> servers =
            new Dictionary<int, WebSocketServer> ();
        static Dictionary < (int, string), IWebSocketConnection > pool =
            new Dictionary < (int, string), IWebSocketConnection > ();
        //                  port, path
        static WebsocketDaemon () {
            Fleck.FleckLog.LogAction = (level, m, e) => {
                switch (level) {
                case LogLevel.Info:
                    Logger.Info (m);
                    break;
                case LogLevel.Warn:
                    Logger.Warn (m);
                    break;
                case LogLevel.Error:
                    Logger.Error (m);
                    break;
                case LogLevel.Debug:
                    Logger.Debug (m);
                    break;
                }
            }; // unify logs
        }
        /// <summary>
        /// 
        /// </summary>
        /// <remark>不要实例化</remark>
        public class WebsocketServerInstance {
            int port;
            string path;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="port"></param>
            /// <param name="path"></param>
            public WebsocketServerInstance (int port, string path) {
                this.port = port;
                this.path = path.Trim ('/');
                if (servers.ContainsKey (port) == false) {
                    servers[port] = new WebSocketServer ($"ws://0.0.0.0:{port}");
                    servers[port].RestartAfterListenError = true;
                    servers[port].Start (socket => {
                        socket.OnOpen = () => {
                            Logger.Debug (
                                $"来自{socket.ConnectionInfo.ClientIpAddress}的连接"
                            );
                            pool[(port, socket.ConnectionInfo.Path.Trim ('/'))] = socket;
                        };
                        socket.OnClose = () => {
                            Logger.Debug (
                                $"{socket.ConnectionInfo.ClientIpAddress}关闭连接"
                            );
                            pool.Remove ((port, socket.ConnectionInfo.Path.Trim ('/')));
                        };
                    });
                }
            }
            ///
            public IWebSocketConnection socket {
                get {
                    Config.TimeOut (
                        () => pool.ContainsKey ((port, path)),
                        $"没有在{Config.timeOut}秒内收到发往0.0.0.0:{port}/{path}的连接请求"
                    ).Wait (); //请检查网络连通性或调整cqhttp.Cyan.Config.timeOut
                    return pool[(port, path)];
                }
            }
        }
    }
}