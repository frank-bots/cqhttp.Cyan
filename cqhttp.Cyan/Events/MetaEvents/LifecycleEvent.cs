using cqhttp.Cyan.Events.Base;
namespace cqhttp.Cyan.Events.MetaEvents {
    public class LifecycleEvent : CQEvent {
        public bool enabled { get; private set; }
        public LifecycleEvent (long time, bool sub_type) : base (time, PostType.meta_event) {
            this.enabled = sub_type;
        }
    }
}