using System;
using System.Collections.Generic;
using cqhttp.Cyan.Utils;
using Fleck;

namespace cqhttp.Cyan.Clients.WebsocketUtils {
    static class WebsocketDaemon {
        static Dictionary<int, WebSocketServer> servers =
            new Dictionary<int, WebSocketServer> ();
        static Dictionary<(int, string), IWebSocketConnection> pool =
            new Dictionary<(int, string), IWebSocketConnection> ();
        static Dictionary<(int, string), Action<string>> handlers =
            new Dictionary<(int, string), Action<string>> ();
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
        internal class WebsocketServerInstance {
            int port;
            string path;
            internal WebsocketServerInstance (int port, string path, string token, Action<string> OnMessage) {
                this.port = port;
                this.path = path.Trim ('/');
                if (handlers.ContainsKey ((port, path)))
                    Log.Warn ($"Overriding Reverse WS handler: {port}:{path}");
                handlers[(port, path)] = OnMessage;
                if (servers.ContainsKey (port) == false) {
                    servers[port] = new WebSocketServer ($"ws://0.0.0.0:{port}");
                    servers[port].Start (socket => {
                        socket.OnOpen = () => {
                            Log.Debug (
                                $"来自{socket.ConnectionInfo.ClientIpAddress}的连接"
                            );
                            string remote_token;
                            if (
                                token != "" &&
                                (socket.ConnectionInfo.Headers.TryGetValue ("Authorization", out remote_token) &&
                                remote_token.Contains (token)) == false
                            ) {
                                Log.Error ("连接token错误，身份验证失败");
                                socket.Close ();
                            } else
                                pool[(port, socket.ConnectionInfo.Path.Trim ('/'))] = socket;
                        };
                        socket.OnClose = () => {
                            var conn_path = socket.ConnectionInfo.Path.Trim ('/');
                            Log.Warn (
                                $"{socket.ConnectionInfo.ClientIpAddress}/{conn_path}关闭连接"
                            );
                            if (pool.ContainsKey ((port, conn_path)))
                                pool.Remove ((port, conn_path));
                        };
                        socket.OnMessage = message => {
                            if (handlers.ContainsKey ((port, socket.ConnectionInfo.Path.Trim ('/'))))
                                handlers[(port, socket.ConnectionInfo.Path.Trim ('/'))] (message);
                        };
                    });
                }
            }
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