using System.Collections.Generic;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary>
    /// 插件运行状态
    /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E5%93%8D%E5%BA%94%E6%95%B0%E6%8D%AE27</see>
    /// </summary>
    [JsonObject]
    public class Status {
        /// 
        public bool app_initialized { get; set; }
        /// 
        public bool app_enabled { get; set; }
        ///
        public Dictionary<string, bool> plugins_good { get; set; }
        ///
        public bool app_good { get; set; }
        ///
        public bool online { get; set; }
        ///
        public bool good { get; set; }
    }
    /// <summary>心跳包，请在设置中开启</summary>
    public class HeartbeatEvent : MetaEvent {
        /// <summary></summary>
        public override string meta_event_type { get; } = "heartbeat";
        /// <summary></summary>
        public Status status { get; private set; }
        ///
        public long interval { get; private set; }
        /// <summary></summary>
        public HeartbeatEvent (long time, Status status, long interval) : base (time) {
            this.status = status;
            this.interval = interval;
        }
    }
}