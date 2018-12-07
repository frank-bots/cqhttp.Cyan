namespace cqhttp.Cyan.Events.Base {
    public class GroupNoticeEvent : NoticeEvent {
        public int group_id { get; private set; }
        public GroupNoticeEvent () : base () { }
        public GroupNoticeEvent (int time, Enums.NoticeType noticeType, int group_id, int user_id):
            base (time, noticeType, user_id) {
                this.group_id = group_id;
            }
    }
}