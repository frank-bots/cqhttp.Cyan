namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary></summary>
    public class LifecycleEvent : MetaEvent {
        /// <summary></summary>
        public bool enabled { get; private set; }
        /// <summary></summary>
        public LifecycleEvent (long time, bool sub_type) : 
            base (time, Enums.PostType.meta_event) {
            this.enabled = sub_type;
        }
    }
}