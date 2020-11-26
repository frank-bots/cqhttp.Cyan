using System;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Clients.Callers {
    internal class WebsocketCaller : ICaller {
        string access_url, access_token;
        ClientWebSocket client = new ClientWebSocket ();
        CancellationTokenSource token_source = new CancellationTokenSource ();
        public WebsocketCaller (
            string access_url,
            string access_token
        ) {
            if (access_url.EndsWith ("/api")) {
                access_url += '/';
            } else if (!access_url.EndsWith ("/api/")) {
                access_url += "/api/";
            }
            this.access_url = access_url;
            this.access_token = access_token;
        }
        public async Task Reconnect () {
            client.Abort ();
            client.Dispose ();
            client = new ClientWebSocket ();
            await client.ConnectAsync (
                new Uri (access_url + (access_token == "" ? "" : "?access_token=" + access_token)),
                token_source.Token
            );
        }
        async Task SendText (string text) {
            await client.SendAsync (
                System.Text.Encoding.UTF8.GetBytes (text),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                token_source.Token
            );
        }
        async Task<string> ReceiveText () {
            string result = "";
            byte[] buffer = new byte[1024 * 3];
            while (true) {
                var res = await client.ReceiveAsync (
                        buffer: buffer,
                        cancellationToken: new CancellationToken ()
                    );
                result += System.Text.Encoding.UTF8.GetString (buffer).TrimEnd ('\0');
                if (res.EndOfMessage)
                    break;
            }
            return result;
        }
        SemaphoreSlim lock_ = new SemaphoreSlim (1, 1);
        /// <summary></summary>
        public async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            var wait = lock_.WaitAsync ();
            try {
                JObject constructor = new JObject ();
                constructor["action"] = x.api_path.Substring (1);
                constructor["params"] = JObject.Parse (x.content);
                constructor["echo"] = System.DateTime.Now.Millisecond;
                await wait;
                if (client.State != WebSocketState.Open)
                    await Reconnect ();
                await SendText (constructor.ToString (Newtonsoft.Json.Formatting.None));
                var resp = await ReceiveText ();
                if (resp.Contains ("authorization failed"))
                    throw new Exceptions.ErrorApicallException ("access token 有误");
                x.response.Parse (JToken.Parse (resp));
            } catch (System.Exception e) {
                Log.Error ("Websocket调用API失败");
                Log.Debug ("调用:" + x.ToString ());
                Log.Debug ("Traceback:" + e.StackTrace);
                throw new Exceptions.NetworkFailureException ("调用API失败");
            } finally {
                lock_.Release ();
            }
            return x.response;
        }
    }
}