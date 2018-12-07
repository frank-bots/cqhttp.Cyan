using cqhttp.Cyan.Enums;
namespace cqhttp.Cyan.Events.Base {
    public class NoticeEvent : CQEvent {
        public NoticeType noticeType { get; private set; }
        public int user_id { get; private set; }
        public NoticeEvent (int time, NoticeType noticeType, int user_id):
            base (time, PostType.notice) {
                this.noticeType = noticeType;
                this.user_id = user_id;
            }
        public NoticeEvent () : base () { }
    }
}