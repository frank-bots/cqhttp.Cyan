using System.Collections.Generic;

namespace cqhttp.Cyan.Events.CQEvents.MetaEvents {
    /// <summary>
    /// 元事件
    /// </summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (Utils.DiscriminatedJsonConverter),
        typeof (MetaEventDiscriminatorOptions)
    )]
    public class MetaEvent : cqhttp.Cyan.Events.CQEvents.Base.CQEvent {
        ///
        public string meta_event_type { get; set; }
    }

    /// <summary></summary>
    [DiscriminatorValue ("lifecycle")]
    public class LifecycleEvent : MetaEvent {
        /// <summary></summary>
        public string sub_type { get; set; }
    }

    /// <summary>心跳包，请在设置中开启</summary>
    [DiscriminatorValue ("heartbeat")]
    public class HeartbeatEvent : MetaEvent {
        /// <summary>
        /// 插件运行状态
        /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E5%93%8D%E5%BA%94%E6%95%B0%E6%8D%AE27</see>
        /// </summary>
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
        /// <summary></summary>
        public Status status { get; set; }
        ///
        public long interval { get; set; }
    }
}