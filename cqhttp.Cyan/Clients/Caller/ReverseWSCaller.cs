using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Clients.WebsocketUtils;
using cqhttp.Cyan.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Clients.Callers {
    class ReverseWSCaller : ICaller {
        WebsocketDaemon.WebsocketServerInstance server;
        object buffer_lock = new object ();
        Dictionary<long, JToken> buffer = new Dictionary<long, JToken> ();
        public ReverseWSCaller (
            int listen_port,
            string api_path,
            string access_token
        ) {
            api_path = api_path.Trim ('/');
            server = new WebsocketDaemon.WebsocketServerInstance (listen_port, api_path);

            if (server.socket.ConnectionInfo.Headers["Authorization"].Contains (
                    access_token
                ) == false)
                throw new Exceptions.ErrorApicallException ("身份验证失败");
            server.socket.OnMessage = (m) => {
                Log.Debug ($"[reverse websocket received API response]:\n{m}");
                JToken t = JToken.Parse (m);
                lock (buffer_lock) {
                    buffer[t["echo"].ToObject<long> ()] = t;
                }
            };
            Log.Info ($"成功连接");
        }
        public async Task<ApiResult> SendRequestAsync (ApiRequest request) {
            JObject constructor = new JObject ();
            long echo = DateTime.Now.ToBinary ();
            constructor["action"] = request.apiPath.Substring (1);
            constructor["params"] = JObject.Parse (request.content);
            constructor["echo"] = echo;
            await server.socket.Send (constructor.ToString (Formatting.None));
            await new Func<bool> (
                () => buffer.ContainsKey (echo)
            ).TimeOut ("API调用超时");
            request.response.Parse (buffer[echo]);
            lock (buffer_lock) {
                buffer.Remove (echo);
            }
            return request.response;
        }
    }
}