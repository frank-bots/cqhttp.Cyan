using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Events.EventListener;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Instance {
    /// <summary>websocket协议调用api</summary>
    public class CQWebsocketClient : CQApiClient {
        private string accessUrl;
        /// <summary></summary>
        public CQWebsocketClient (
                string accessUrl,
                string accessToken = "",
                string eventUrl = "",
                bool use_group_table = false,
                bool use_message_table = false
            ):
            base (accessToken, use_group_table, use_message_table) {
                if (accessUrl.EndsWith ("/api")) {
                    accessUrl += '/';
                } else if (!accessUrl.EndsWith ("/api/")) {
                    accessUrl += "/api/";
                }
                this.accessUrl = accessUrl;
                if (!string.IsNullOrEmpty (eventUrl)) {
                    if (eventUrl.EndsWith ("/event")) {
                        eventUrl += '/';
                    } else if (!eventUrl.EndsWith ("/event")) {
                        eventUrl += "/event/";
                    }
                    eventUrl += !string.IsNullOrEmpty (accessToken) ? "?access_token=" + accessToken : "";
                    this.__eventListener = new WebsocketListener (eventUrl);
                    (this.__eventListener as WebsocketListener).api_call_func
                        = this.SendRequestAsync;
                    this.__eventListener.StartListen (__HandleEvent);
                }
                if (base.Initiate ().Result == false)
                    throw new Exceptions.ErrorApicallException ("初始化失败");
                Logger.Info ("成功连接");
            }

        /// <summary></summary>
        public override async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            var ret = await WSSendJson (accessUrl, x, accessToken);
            x.response = ret;
            RequestPreprocess (x);
            return ret;
        }

        /// <summary></summary>
        ~CQWebsocketClient () {
            CleanUp ();
        }
        private static async Task<ApiResult> WSSendJson (string host, ApiRequest request, string apiToken = "") {
            string dest = host +
                (apiToken == "" ? "" : "?access_token=" + apiToken);
            JObject constructor = new JObject ();
            constructor["action"] = request.apiPath.Substring (1);
            constructor["params"] = JObject.Parse (request.content);
            int timemark = await WebsocketUtils.ConnectionPool.SendJsonAsync (
                dest, constructor
            ); // how to make use of timestamp?
            string resp = await WebsocketUtils.ConnectionPool.GetResponse (dest);
            if (resp.Contains ("authorization failed"))
                throw new Exceptions.ErrorApicallException ("身份验证失败");
            request.response.Parse (resp);
            return request.response;
        }
        private async void CleanUp () {
            Logger.Info ("开始关闭Websocket连接");
            await WebsocketUtils.ConnectionPool.CloseAsync (
                this.accessUrl + (this.accessToken == "" ? "" : "?access_token=" + this.accessToken)
            );
        }
    }
}