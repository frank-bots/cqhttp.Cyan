using System.Net;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.WebSockets;

namespace cqhttp.Cyan.Clients.Callers {
    class ReverseWSCaller : WebsocketCallerBase {
        HttpListener listener;
        public ReverseWSCaller (
            int listen_port,
            string api_path,
            string access_token
        ) {
            api_path = api_path.Trim ('/');
            listener = new HttpListener ();
            listener.Prefixes.Add ($"http://+:{listen_port}/{api_path}/");
            listener.Start ();
            Task.Run (async () => {
                while (listener.IsListening) {
                    var context = await listener.GetContextAsync ();
                    if (!context.Request.Url.AbsolutePath.StartsWith ($"/{api_path}")) {
                        context.Response.StatusCode = 404;
                        context.Response.Close ();
                    } else if (context.Request.Headers["X-Client-Role"] != "API") {
                        Log.Error ("X-Client-Role != API");
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
                this.socket = socket;
            else
                Log.Warn ("Websocket连接出现了问题");
        }
    }
}