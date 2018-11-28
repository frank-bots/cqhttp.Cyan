using System;
using System.Net.WebSockets;
using System.Threading;

namespace cqhttp.Cyan.Communication {
    public class WebSocketClient {
        private ClientWebSocket wsClient;

        public WebSocketClient (string url) {
            wsClient = new ClientWebSocket ();
            wsClient.ConnectAsync (new Uri(url), new CancellationToken ()).Wait();
        }
    }
}