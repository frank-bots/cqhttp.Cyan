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
        public bool app_initialized { get; private set; }
        /// 
        public bool app_enabled { get; private set; }
        ///
        public Dictionary<string,bool> plugins_good { get; private set; }
        ///
        public bool app_good { get; private set; }
        ///
        public bool online { get; private set; }
        ///
        public bool good { get; private set; }
    }
}