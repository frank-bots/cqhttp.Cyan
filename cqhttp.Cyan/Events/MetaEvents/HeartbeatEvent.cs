using cqhttp.Cyan.Events.Base;

namespace cqhttp.Cyan.Events.MetaEvents {
    public class HeartbeatEvent : CQEvent {
        public Status status { get; private set; }
        public HeartbeatEvent (long time, Status status):
            base (time, PostType.meta_event) {
                this.status = status;
            }
    }
}