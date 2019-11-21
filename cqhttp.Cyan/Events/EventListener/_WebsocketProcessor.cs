using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;

namespace cqhttp.Cyan.Events.EventListener {
    ///
    public class _WebsocketProcessor : CQEventListener {
        /// <summary>
        /// 处理快速回复的时候需要调用
        /// </summary>
        public System.Func<ApiRequest, Task<ApiResult>> api_call_func;
        ///
        public _WebsocketProcessor (string x) : base (x) { }
        ///
        protected async void Process (string message) {
            try {
                if (string.IsNullOrEmpty (message))
                    return;
                var response = await listen_callback (CQEventHandler.HandleEvent (message));
                if (response is CQResponses.EmptyResponse == false)
                    await Task.Run (() => {
                        api_call_func (new ApiCall.Requests.HandleQuickOperationRequest (
                            context: message,
                            operation: response.content
                        ));
                    });
            } catch (System.Exception e) {
                Logger.Error (
                    $"处理事件时发生未处理的异常{e},错误信息为{e.Message}"
                );
            }
        }
    }
}