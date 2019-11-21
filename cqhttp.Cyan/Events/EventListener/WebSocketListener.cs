using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary></summary>
    public class WebsocketListener : _WebsocketProcessor {
        private ClientWebSocket client;
        private string dest_url;

        /// <summary></summary>
        public WebsocketListener (string dest_url) : base ("") {
            this.dest_url = dest_url;
            client = new ClientWebSocket ();
        }
        /// <summary></summary>
        public override void StartListen (System.Func<CQEvents.Base.CQEvent, Task<CQResponses.Base.CQResponse>> callback) {
            Logger.Info ($"建立与事件上报服务器{dest_url}的websocket连接");
            listen_callback = callback;
            lock (listen_lock) {
                client.ConnectAsync (
                    new System.Uri (dest_url), new System.Threading.CancellationToken ()
                );
                listen_task = System.Threading.Tasks.Task.Run (async () => {
                    byte[] recv_buffer = new byte[2048];
                    List<byte> message = new List<byte> ();
                    while (true) {
                        if (client.State == WebSocketState.Open) {
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
                        }
                    }
                });
            }
        }
    }
}