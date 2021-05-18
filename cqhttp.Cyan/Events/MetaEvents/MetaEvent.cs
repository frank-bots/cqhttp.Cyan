using cqhttp.Cyan.Enums;
using JsonSubTypes;

namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary>
    /// 元事件
    /// </summary>
    [Newtonsoft.Json.JsonConverter (typeof (JsonSubtypes), "meta_event_type")]
    [JsonSubtypes.KnownSubType (typeof (HeartbeatEvent), "heartbeat")]
    [JsonSubtypes.KnownSubType (typeof (LifecycleEvent), "lifecycle")]
    [JsonSubtypes.FallBackSubType (typeof (CQEvents.UnknownEvent))]
    public class MetaEvent : cqhttp.Cyan.Events.CQEvents.Base.CQEvent {
        /// <summary></summary>
        public override string post_type { get; } = "meta_event";
        /// <summary></summary>
        public virtual string meta_event_type { get; }
        /// <summary></summary>
        public MetaEvent (long time) : base (time) { }
    }
}