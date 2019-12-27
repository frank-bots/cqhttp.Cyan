using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Events.CQResponses.Base;

namespace cqhttp.Cyan.Clients.Listeners {
    class WebsocketListener : Listener {
        ClientWebSocket client;
        public Callers.ICaller caller;
        public WebsocketListener (string event_url, string access_token) {
            if (event_url.EndsWith ("/event")) {
                event_url += '/';
            } else if (!event_url.EndsWith ("/event/")) {
                event_url += "/event/";
            }
            event_url += !string.IsNullOrEmpty (access_token) ? "?access_token=" + access_token : "";
            
            client = new ClientWebSocket ();
            client.ConnectAsync (
                new System.Uri (event_url), new System.Threading.CancellationToken ()
            );
            System.Threading.Tasks.Task.Run (async () => {
                byte[] recv_buffer = new byte[2048];
                List<byte> message = new List<byte> ();
                while (true) {
                    switch (client.State) {
                    case WebSocketState.Open:
                        var t = await client.ReceiveAsync (
                            recv_buffer,
                            new System.Threading.CancellationToken ()
                        );
                        if (!t.EndOfMessage) {
                            message.AddRange (recv_buffer);
                        } else {
                            message.AddRange (recv_buffer.ToList ().GetRange (0, t.Count));
                            Process (System.Text.Encoding.UTF8.GetString (message.ToArray ()));
                            message.Clear ();
                        }
                        break;
                    }
                }
            });
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