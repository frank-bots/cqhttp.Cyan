namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// <see>https://cqhttp.cc/docs/4.6/#/Post?id=%E4%BA%8B%E4%BB%B6%E5%88%97%E8%A1%A8</see>
    /// </summary>
    public abstract class GroupNoticeEvent : NoticeEvent {
        /// <summary>
        /// 事件发生的群号码
        /// </summary>
        public long group_id { get; private set; }
        /// <summary></summary>
        public GroupNoticeEvent () : base () { }
        /// <summary></summary>
        public GroupNoticeEvent (long time, Enums.NoticeType noticeType, long group_id, long user_id):
            base (time, noticeType, user_id) {
                this.group_id = group_id;
            }
    }
}