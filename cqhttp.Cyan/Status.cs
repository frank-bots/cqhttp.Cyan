using System.Collections.Generic;
using Newtonsoft.Json;

namespace cqhttp.Cyan {
    /// <summary>
    /// 插件运行状态
    /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E5%93%8D%E5%BA%94%E6%95%B0%E6%8D%AE27</see>
    /// </summary>
    [JsonObject]
    public class Status {
        /// 
        [JsonProperty("app_initialized")]
        public bool app_initialized { get; private set; }
        /// 
        [JsonProperty("app_enabled")]
        public bool app_enabled { get; private set; }
        ///
        [JsonProperty("plugins_good")]
        public Dictionary<string,bool> plugins_good { get; private set; }
        ///
        [JsonProperty("app_good")]
        public bool app_good { get; private set; }
        ///
        [JsonProperty("online")]
        public bool online { get; private set; }
        ///
        [JsonProperty("good")]
        public bool good { get; private set; }
    }
}