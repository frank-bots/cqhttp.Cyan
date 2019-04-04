using cqhttp.Cyan.ApiCall.Requests.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 重启酷 Q，并以当前登录号自动登录（需勾选快速登录）
    /// </summary>
    [JsonObject]
    public class SetRestartRequest : ApiRequest {
        [JsonProperty] bool clean_log;
        [JsonProperty] bool clean_cache;
        [JsonProperty] bool clean_event;

        /// <param name="clean_log">是否在重启时清空酷 Q 的日志数据库（logv1.db）</param>
        /// <param name="clean_cache">是否在重启时清空酷 Q 的缓存数据库（cache.db）</param>
        /// <param name="clean_event">是否在重启时清空酷 Q 的事件数据库（eventv2.db）</param>
        public SetRestartRequest (bool clean_log, bool clean_cache, bool clean_event):
            base ("/set_restart") {
                this.response = new Results.EmptyResult ();
                this.clean_log = clean_log;
                this.clean_cache = clean_cache;
                this.clean_event = clean_event;
            }
    }
}