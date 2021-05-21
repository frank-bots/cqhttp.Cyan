using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Clients.Callers {
    abstract class WebsocketCallerBase : ICaller {
        WebSocket _socket;
        protected WebSocket socket {
            get {
                new System.Func<bool> (
                    () => _socket?.State == WebSocketState.Open
                ).TimeOut (
                    $"没有在{Config.timeout}秒内建立Websocket连接，无法调用API"
                ).Wait (); //请检查网络连通性或调整cqhttp.Cyan.Config.timeOut
                return _socket;
            }
            set => _socket = value;
        }
        protected CancellationTokenSource ctoken_source = new CancellationTokenSource ();
        SemaphoreSlim lock_ = new SemaphoreSlim (1, 1);
        // Dictionary<long, JToken> buffer = new Dictionary<long, JToken> ();

        async Task SendText (string text) {
            await socket.SendAsync (
                System.Text.Encoding.UTF8.GetBytes (text),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                ctoken_source.Token
            );
        }

        async Task<string> ReceiveText () {
            string result = "";
            byte[] buffer = new byte[1024 * 4];
            WebSocketReceiveResult res = null;
            while (res?.EndOfMessage != true) {
                res = await socket.ReceiveAsync (
                    buffer: buffer,
                    cancellationToken: ctoken_source.Token
                );
                result += System.Text.Encoding.UTF8.GetString (buffer).TrimEnd ('\0');
            }
            if (result.Contains ("authorization failed"))
                throw new Exceptions.ErrorApicallException ("access token 有误");
            return result;
        }

        /// <summary></summary>
        public async Task<ApiResult> SendRequestAsync (ApiRequest request) {
            JObject constructor = new JObject ();
            long echo = DateTime.Now.ToBinary ();
            constructor["action"] = request.api_path.Substring (1);
            constructor["params"] = JObject.Parse (request.content);
            constructor["echo"] = echo;
            await lock_.WaitAsync ();
            await SendText (constructor.ToString (Formatting.None));
            var response = await ReceiveText ();
            lock_.Release ();
            request.response.Parse (JToken.Parse (response));
            return request.response;
        }
    }
}