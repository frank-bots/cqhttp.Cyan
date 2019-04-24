using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;

namespace cqhttp.Cyan.Instance {
    /// <summary>
    /// 反向websocket连接方式
    /// </summary>
    public class CQReverseWSClient : CQApiClient {
        /// <summary>
        /// 当cqhttp中use_ws_reverse配置项为true时使用
        /// </summary>
        /// <param name="apiPath">即ws_reverse_api_url</param>
        /// <param name="eventPath">即ws_reverse_event_url</param>
        /// <param name="accessToken"></param>
        /// <param name="use_group_table"></param>
        /// <param name="use_message_table"></param>
        /// <returns></returns>
        public CQReverseWSClient (
            string apiPath,
            string eventPath,
            string accessToken = "",
            bool use_group_table = false,
            bool use_message_table = false
        ) : base (accessToken, use_group_table, use_message_table) {
            throw new NotImplementedException ();
        }
        ///
        public override async Task<ApiResult> SendRequestAsync (ApiRequest x) {
            await Task.Run (() => { });
            throw new NotImplementedException ();
        }
    }
}