using cqhttp.Cyan.Enums;
namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary></summary>
    public abstract class NoticeEvent : CQEvent {
        /// <summary></summary>
        public NoticeType noticeType { get; private set; }
        /// <summary></summary>
        public long user_id { get; private set; }
        /// <summary></summary>
        public NoticeEvent (long time, NoticeType noticeType, long user_id):
            base (time, PostType.notice) {
                this.noticeType = noticeType;
                this.user_id = user_id;
            }
        /// <summary></summary>
        public NoticeEvent () : base () { }
    }
}