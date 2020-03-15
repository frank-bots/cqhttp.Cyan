using System.Collections.Generic;
using cqhttp.Cyan.Utils;
using Fleck;

namespace cqhttp.Cyan.Clients.WebsocketUtils {
    static class WebsocketDaemon {
        static Dictionary<int, WebSocketServer> servers =
            new Dictionary<int, WebSocketServer> ();
        static Dictionary<(int, string), IWebSocketConnection> pool =
            new Dictionary<(int, string), IWebSocketConnection> ();
        //                  port, path
        static WebsocketDaemon () {
            Fleck.FleckLog.LogAction = (level, m, e) => {
                switch (level) {
                case LogLevel.Info: Log.Info (m); break;
                case LogLevel.Warn: Log.Warn (m); break;
                case LogLevel.Error: Log.Error (m); break;
                case LogLevel.Debug: Log.Debug (m); break;
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
                        $"没有在{Config.timeout}秒内收到发往0.0.0.0:{port}/{path}的连接请求"
                    ).Wait (); //请检查网络连通性或调整cqhttp.Cyan.Config.timeOut
                    return pool[(port, path)];
                }
            }
        }
    }
}