using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 重启 HTTP API 插件
    /// </summary>
    public class SetRestartPluginRequest : ApiRequest {
        [Newtonsoft.Json.JsonProperty] long delay;
        /// <param name="delay">要延迟的毫秒数，如果默认情况下无法重启，可以尝试设置延迟为 2000 左右</param>
        public SetRestartPluginRequest (long delay = 0):
            base ("/set_restart_plugin") {
                this.response = new Results.EmptyResult ();
                this.delay = delay;
            }
    }
}