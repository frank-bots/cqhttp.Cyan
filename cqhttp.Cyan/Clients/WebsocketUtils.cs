using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Utils;
using Fleck;

namespace cqhttp.Cyan.Clients.WebsocketUtils {
    static class WebsocketDaemon {
        static Dictionary<int, WebSocketServer> servers =
            new Dictionary<int, WebSocketServer> ();
        static Dictionary < (int, string), IWebSocketConnection > pool =
            new Dictionary < (int, string), IWebSocketConnection > ();
        //                  port, path
        static WebsocketDaemon () {
            Fleck.FleckLog.LogAction = (level, m, e) => {
                switch (level) {
                case LogLevel.Info:
                    Log.Info (m);
                    break;
                case LogLevel.Warn:
                    Log.Warn (m);
                    break;
                case LogLevel.Error:
                    Log.Error (m);
                    break;
                case LogLevel.Debug:
                    Log.Debug (m);
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
                            Log.Debug (
                                $"来自{socket.ConnectionInfo.ClientIpAddress}的连接"
                            );
                            pool[(port, socket.ConnectionInfo.Path.Trim ('/'))] = socket;
                        };
                        socket.OnClose = () => {
                            Log.Debug (
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
                    new System.Func<bool> (() => pool.ContainsKey ((port, path))).TimeOut (
                        $"没有在{Config.timeOut}秒内收到发往0.0.0.0:{port}/{path}的连接请求"
                    ).Wait (); //请检查网络连通性或调整cqhttp.Cyan.Config.timeOut
                    return pool[(port, path)];
                }
            }
        }
    }
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
                await new System.Func<bool> (
                    () => pool[uri].State != WebSocketState.Connecting
                ).TimeOut ("websocket 连接超时");
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
        public static async Task<string> RequestAsync (string uri, string obj) {
            await lock_.WaitAsync ();
            try {
                string result = "";
                await EnsureConnected (uri);
                await pool[uri].SendAsync (
                    buffer: System.Text.Encoding.UTF8.GetBytes (obj),
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
                    result += System.Text.Encoding.UTF8.GetString (buffer).TrimEnd ('\0');
                    if (res.EndOfMessage)
                        break;
                }
                return result;
            } catch (System.Exception e) {
                Log.Error ("Websocket调用API失败");
                Log.Debug ("调用:" + obj.ToString ());
                Log.Debug ("Traceback:" + e.StackTrace);
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