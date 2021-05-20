using System.Net.WebSockets;
using System.Threading.Tasks;

namespace cqhttp.Cyan.Clients.Listeners {
    class WebsocketListener : WebsocketListenerBase {
        public WebsocketListener (string event_url, string access_token) {
            event_url.TrimEnd ('/');
            if (!event_url.EndsWith ("/event")) {
                event_url += "/event";
            }
            event_url += !string.IsNullOrEmpty (access_token) ? "?access_token=" + access_token : "";
            Task.Run (async () => {
                ClientWebSocket client = new ClientWebSocket ();
                while (ctoken_source.Token.IsCancellationRequested == false) {
                    await client.ConnectAsync (
                        new System.Uri (event_url),
                        ctoken_source.Token
                    );
                    await ListenEventAsync (client, ctoken_source.Token);
                }
            }, ctoken_source.Token);
        }
    }
}