namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary></summary>
    public class LifecycleEvent : MetaEvent {
        /// <summary></summary>
        public string sub_type { get; private set; }
        /// <summary></summary>
        public LifecycleEvent (long time, string sub_type)
        : base (time, Enums.PostType.meta_event) { this.sub_type = sub_type; }
    }
}