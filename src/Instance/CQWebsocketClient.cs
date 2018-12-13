using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.Events.EventListener;

namespace cqhttp.Cyan.Instance {
    /// <summary>websocket协议调用api</summary>
    public class CQWebsocketClient : CQApiClient {
        /// <summary></summary>
        public CQWebsocketClient (string accessUrl, string accessToken = "", string dest_url = "", string secret = ""):
            base (accessUrl, accessToken) {
                if (dest_url.Length != 0) {
                    this.__eventListener = new WebSocketListener (dest_url, secret);
                    this.__eventListener.StartListen (__HandleEvent);
                }
            }
        /// <summary></summary>
        public override async Task<ApiResponse> SendRequestAsync (ApiRequest x) {
                return await WebSocketApiSender.WSSendJson (accessUrl, x, accessToken);
            }
            /// <summary></summary>
            ~CQWebsocketClient () {
                WebSocketApiSender.CleanUp ();
            }
    }
}