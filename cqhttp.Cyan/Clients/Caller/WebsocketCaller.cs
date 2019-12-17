using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Clients.WebsocketUtils;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Clients.Callers {
    internal class WebsocketCaller : ICaller {
        string access_url, access_token;
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
        /// <summary></summary>
        public async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            JObject constructor = new JObject ();
            constructor["action"] = x.apiPath.Substring (1);
            constructor["params"] = JObject.Parse (x.content);
            constructor["echo"] = System.DateTime.Now.Millisecond;
            var resp = await ConnectionPool.RequestAsync (
                access_url + (access_token == "" ? "" : "?access_token=" + access_token),
                constructor.ToString (Newtonsoft.Json.Formatting.None)
            );
            if (resp.Contains ("authorization failed"))
                throw new Exceptions.ErrorApicallException ("身份验证失败");
            var ret = JToken.Parse (resp);
            x.response.Parse (ret);
            return x.response;
        }
        ~WebsocketCaller () {
            CleanUp ();
        }
        private async void CleanUp () {
            Logger.Info ("开始关闭Websocket连接");
            await ConnectionPool.CloseAsync (
                this.access_url + (this.access_token == "" ? "" : "?access_token=" + this.access_token)
            );
        }
    }
}