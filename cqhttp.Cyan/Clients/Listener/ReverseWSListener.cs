using System.Threading;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace cqhttp.Cyan.Clients.Listeners {
    class ReverseWSListener : WebsocketListenerBase {
        HttpListener listener;
        public ReverseWSListener (int bind_port, string event_path, string access_token) {
            event_path = event_path.Trim ('/');
            if (bind_port != -1) {
                listener = new HttpListener ();
                listener.Prefixes.Add ($"http://+:{bind_port}/{event_path}/");
                listener.Start ();
                Task.Run (async () => {
                    while (listener.IsListening) {
                        var context = await listener.GetContextAsync ();
                        if (!context.Request.Url.AbsolutePath.StartsWith ($"/{event_path}")) {
                            context.Response.StatusCode = 404;
                            context.Response.Close ();
                        } else if (context.Request.Headers["X-Client-Role"] != "Event") {
                            Log.Error ("X-Client-Role != Event");
                            Log.Warn ("Cyan暂不支持 ws_reverse_use_universal_client");
                            context.Response.StatusCode = 403;
                            context.Response.Close ();
                        } else if (!context.Request.IsWebSocketRequest) {
                            Log.Error ("非Websocket连接请求");
                            context.Response.StatusCode = 404;
                            context.Response.Close ();
                        } else { var _ = ProcessContextAsync (context, ctoken_source.Token); }
                    }
                });
            }
        }

        async Task ProcessContextAsync (HttpListenerContext context, CancellationToken token) {
            var remote = context.Request.RemoteEndPoint;
            Log.Info ($"来自<{remote.Address}:{remote.Port}>的反向Websocket连接");
            WebSocket socket = null;
            try {
                WebSocketContext ws_context = await context.AcceptWebSocketAsync (null);
                socket = ws_context.WebSocket;
            } catch (Exception e) {
                Log.Error ($"Websocket连接建立失败{e}\n{e.Message}");
                context.Response.StatusCode = 500;
                context.Response.Close ();
            }
            if (socket != null)
                await ListenEventAsync (socket, ctoken_source.Token);
            else
                Log.Warn ("Websocket连接出现了问题");
            Log.Info ($"连接<{remote.Address}:{remote.Port}>已关闭");
        }
    }
}