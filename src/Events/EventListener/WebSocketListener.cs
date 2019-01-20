using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary></summary>
    public class WebSocketListener : CQEventListener {
        private ClientWebSocket client;
        private string dest_url;
        /// <summary></summary>
        public WebSocketListener (string dest_url) : base ("") {
            this.dest_url = dest_url;
            client = new ClientWebSocket ();
        }
        /// <summary></summary>
        public override void StartListen (System.Func<CQEvents.Base.CQEvent, CQResponses.Base.CQResponse> callback) {
            Logger.Log (Verbosity.INFO, $"建立与事件上报服务器{dest_url}的websocket连接");
            listen_callback = callback;
            lock (listen_lock) {
                client.ConnectAsync (
                    new System.Uri (dest_url), new System.Threading.CancellationToken ()
                );
                listen_task = System.Threading.Tasks.Task.Run (() => {
                    byte[] recv_buffer = new byte[2048];
                    List<byte> message = new List<byte> ();
                    while (true) {
                        if (client.State == WebSocketState.Open)
                        {
                            var t = client.ReceiveAsync(
                                    recv_buffer,
                                    new System.Threading.CancellationToken()
                                );
                            if(!t.Result.EndOfMessage)
                            {
                                message.AddRange(recv_buffer);
                            }
                            else
                            {
                                message.AddRange(recv_buffer.ToList().GetRange(0, t.Result.Count));
                                Process(System.Text.Encoding.UTF8.GetString(message.ToArray()));
                                message.Clear();
                            }
                        }
                    }
                });
            }
        }
        private async void Process (string message) {
            try {
                if (string.IsNullOrEmpty(message))
                    return;
                await Task.Run (() =>
                    listen_callback (CQEventHandler.HandleEvent (message))
                ); //Websocket下不会处理响应！！！！！
            } catch (System.Exception e) {
                Logger.Log (
                    Verbosity.ERROR,
                    $"处理事件时发生未处理的异常{e},错误信息为{e.Message}"
                );
            }
        }
    }
}