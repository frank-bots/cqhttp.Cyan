namespace cqhttp.Cyan.Events.Base {
    /// <summary>
    /// 事件
    /// </summary>
    public enum NoticeType {
        group_upload,
        group_admin,
        group_decrease,
        group_increase,
        friend_add
    }
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