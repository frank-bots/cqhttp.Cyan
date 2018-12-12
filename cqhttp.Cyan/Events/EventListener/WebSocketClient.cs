using System;
using System.Net.WebSockets;
using System.Threading;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary>
    /// WebSocket监听上报消息
    /// </summary>
    public class WebSocketClient {
        private ClientWebSocket wsClient;
        /// <param name="url">监听地址</param>
        public WebSocketClient (string url) {
            wsClient = new ClientWebSocket ();
            wsClient.ConnectAsync (new Uri(url), new CancellationToken ()).Wait();
        }
    }
}