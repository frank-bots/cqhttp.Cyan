using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan {
    /// <summary></summary>
    public abstract class CQApiClient {
        /// <summary>
        /// 酷Q配置中的access_token
        /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E8%AF%B7%E6%B1%82%E6%96%B9%E5%BC%8F</see>
        /// </summary>
        public string accessToken = "";
        /// <summary>
        /// API地址
        /// </summary>
        public string accessUrl;
        /// <summary>
        /// 当前实例的QQ号
        /// </summary>
        public long self_id { get; private set; }
        /// <summary>
        /// 当前实例的QQ昵称
        /// </summary>
        public string self_nick { get; private set; }
        /// <summary></summary>
        public CQApiClient (string accessUrl, string accessToken = "") {
            this.accessToken = accessToken;
            this.accessUrl = accessUrl;
            if(!Initiate().Result)throw new ErrorApicallException();
        }
        private async Task<bool> Initiate () {
            ApiResponse loginInfo = await SendRequestAsync (new GetLoginInfoRequest ());
            if (loginInfo.retcode != 0) return false;
            this.self_id = loginInfo.data["user_id"].ToObject<long> ();
            this.self_nick = loginInfo.data["nickname"].ToString();
            return true;
        }
        /// <summary>通用发送请求函数，一般不需调用</summary>
        public virtual Task<ApiResponse> SendRequestAsync (ApiRequest x) {
            throw new NotImplementedException ();
        }
        /// <summary>发送消息(自行构造)</summary>
        public async Task<ApiResponse> SendMessageAsync (
            MessageType messageType,
            long target,
            Message message
        ) {
            return await SendRequestAsync (new SendmsgRequest (messageType, target, message));
        }
        /// <summary>发送纯文本消息</summary>
        public async Task<ApiResponse> SendTextAsync (
            MessageType messageType,
            long target,
            string text
        ) {
            return await SendRequestAsync (new SendmsgRequest (messageType, target,
                new Message { data = new System.Collections.Generic.List<Messages.CQElements.Base.Element> { new Messages.CQElements.ElementText (text) } }
            ));
        }
        
    }
}