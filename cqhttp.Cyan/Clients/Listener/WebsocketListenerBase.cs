using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Events.CQResponses.Base;

namespace cqhttp.Cyan.Clients.Listeners {
    abstract class WebsocketListenerBase : Listener {
        public Callers.ICaller caller = null;
        protected CancellationTokenSource ctoken_source = new CancellationTokenSource ();

        ~WebsocketListenerBase () {
            ctoken_source.Cancel ();
        }
        protected async Task ListenEventAsync (WebSocket client, CancellationToken token) {
            byte[] recv_buffer = new byte[2048];
            List<byte> message = new List<byte> ();
            while (token.IsCancellationRequested == false) {
                switch (client.State) {
                case WebSocketState.CloseReceived:
                    await client.CloseAsync (WebSocketCloseStatus.NormalClosure, "ok", token);
                    return;
                case WebSocketState.Open:
                    var res = await client.ReceiveAsync (
                            recv_buffer, token
                        );
                    if (!res.EndOfMessage) {
                        message.AddRange (recv_buffer);
                    } else {
                        message.AddRange (recv_buffer.ToList ().GetRange (0, res.Count));
                        Process (System.Text.Encoding.UTF8.GetString (message.ToArray ()));
                        message.Clear ();
                    }
                    break;
                default:
                    throw new Exceptions.NetworkFailureException ("undesired websocket state");
                }
            }
        }

        async void Process (string message) {
            try {
                if (string.IsNullOrEmpty (message))
                    return;
                CQResponse response = await GetResponse (message);
                if (response is EmptyResponse == false) {
                    if (caller != null)
                        await caller.SendRequestAsync (
                            new ApiCall.Requests.HandleQuickOperationRequest (
                                context: message,
                                operation: response.content
                            ));
                    else
                        Log.Warn ("WS无法快速响应（caller为null）");
                }
            } catch (System.Exception e) {
                Log.Error (
                    $"处理事件时发生未处理的异常{e},错误信息为{e.Message}"
                );
            }
        }
    }
}