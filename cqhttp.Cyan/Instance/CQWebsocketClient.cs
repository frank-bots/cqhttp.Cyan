using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall;
using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.Instance {
    /// <summary>websocket协议调用api</summary>
    public class CQWebsocketClient : CQApiClient {
        /// <summary></summary>
        public CQWebsocketClient (string accessUrl, string accessToken = "") : 
            base (accessUrl, accessToken) { }
        /// <summary></summary>
        public override async Task<ApiResponse> SendRequestAsync (ApiRequest x) {
            return await WebSocketApiSender.WSSendJson (accessUrl, x, accessToken);
        }
        /// <summary></summary>
        ~CQWebsocketClient(){
            WebSocketApiSender.CleanUp();
        }
    }
}