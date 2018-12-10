using System.Threading.Tasks;
using cqhttp.Cyan.ApiHTTP;
using cqhttp.Cyan.ApiHTTP.Requests;
using cqhttp.Cyan.ApiHTTP.Requests.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan {
    public class ApiClient {
        /// <summary>
        /// 酷Q配置中的access_token
        /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E8%AF%B7%E6%B1%82%E6%96%B9%E5%BC%8F</see>
        /// </summary>
        public string accessToken = "";
        /// <summary>
        /// API地址
        /// </summary>
        public string accessUrl;
        public ApiClient (string accessUrl, string accessToken = "") {
            this.accessToken = accessToken;
            this.accessUrl = accessUrl;
        }
        public async Task<ApiResponse> SendRequestAsync (ApiRequest x) {
            return await ApiSender.PostJsonAsync (
                dest: accessUrl + x.apiUrl,
                param: x.content,
                apiToken: accessToken
            );
        }
        public async Task<ApiResponse> SendMessageAsync (
            MessageType messageType,
            long target,
            Message message
        ) {
            return await SendRequestAsync (new SendmsgRequest (messageType, target, message));
        }
        public async Task<ApiResponse> SendTextAsync (
            MessageType messageType,
            long target,
            string text
        ) {
            return await SendRequestAsync (new SendmsgRequest (messageType, target,
                new Message { data = new System.Collections.Generic.List<Messages.Base.Element> { new Messages.CQElements.ElementText (text) } }
            ));
        }
    }
}