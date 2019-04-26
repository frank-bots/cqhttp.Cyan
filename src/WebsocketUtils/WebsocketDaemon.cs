using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Fleck;
namespace cqhttp.Cyan.WebsocketUtils {

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
                Enums.Verbosity v = Enums.Verbosity.INFO;
                switch (level) {
                    case LogLevel.Info:
                        v = Enums.Verbosity.INFO;
                        break;
                    case LogLevel.Warn:
                        v = Enums.Verbosity.WARN;
                        break;
                    case LogLevel.Error:
                        v = Enums.Verbosity.ERROR;
                        break;
                    case LogLevel.Debug:
                        v = Enums.Verbosity.DEBUG;
                        break;
                }
                Logger.Log (v, m);
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
                            Logger.Log (
                                Enums.Verbosity.DEBUG,
                                $"来自{socket.ConnectionInfo.ClientIpAddress}的连接"
                            );
                            pool[(port, socket.ConnectionInfo.Path.Trim ('/'))] = socket;
                        };
                        socket.OnClose = () => {
                            Logger.Log (
                                Enums.Verbosity.DEBUG,
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