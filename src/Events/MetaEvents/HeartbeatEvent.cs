using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.MetaEvents {
    /// <summary>心跳包，请在设置中开启</summary>
    public class HeartbeatEvent : MetaEvent {
        /// <summary></summary>
        public Status status { get; private set; }
        /// <summary></summary>
        public HeartbeatEvent (long time, Status status):
            base (time, Enums.PostType.meta_event) {
                this.status = status;
            }
    }
}