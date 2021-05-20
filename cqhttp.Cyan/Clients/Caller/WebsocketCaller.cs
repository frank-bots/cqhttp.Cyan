using System.Net.WebSockets;
using System;

namespace cqhttp.Cyan.Clients.Callers {
    internal class WebsocketCaller : WebsocketCallerBase {
        string access_url, access_token;
        public WebsocketCaller (
            string access_url,
            string access_token
        ) {
            access_url.TrimEnd ('/');
            if (!access_url.EndsWith ("/api")) {
                access_url += "/api";
            }
            this.access_url = access_url;
            this.access_token = access_token;
            var uri = new Uri (access_url + (access_token == "" ? "" : "?access_token=" + access_token));
            var socket = new ClientWebSocket ();
            (socket as ClientWebSocket).ConnectAsync (uri, ctoken_source.Token);
            this.socket = socket;
        }
    }
}