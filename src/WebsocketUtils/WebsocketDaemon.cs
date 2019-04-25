using System.Collections.Generic;
using Fleck;
namespace cqhttp.Cyan.WebsocketUtils {

    /// <summary>
    /// As Websocket Server
    /// </summary>
    public static class WebsocketDaemon {
        static Dictionary<int, WebSocketServer> servers;
        static Dictionary < (int, string), IWebSocketConnection > pool;
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
        public class WebsocketServerInstance {
            string port, path;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="port"></param>
            /// <param name="path"></param>
            public WebsocketServerInstance (int port, string path) {
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
                    });
                }
            }
        }
    }
}