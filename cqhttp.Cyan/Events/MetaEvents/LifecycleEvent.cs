namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary></summary>
    [DiscriminatorValue ("lifecycle")]
    public class LifecycleEvent : MetaEvent {
        /// <summary></summary>
        public string sub_type { get; private set; }
    }
}