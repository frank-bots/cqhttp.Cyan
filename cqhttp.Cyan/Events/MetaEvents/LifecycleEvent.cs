namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary></summary>
    public class LifecycleEvent : MetaEvent {
        /// <summary></summary>
        public override string meta_event_type { get; } = "lifecycle";
        /// <summary></summary>
        public bool enabled { get; private set; }
        /// <summary></summary>
        public LifecycleEvent (long time, bool enabled)
        : base (time) { this.enabled = enabled; }
    }
}