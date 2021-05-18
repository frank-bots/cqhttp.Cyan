namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 属性this.user_id中存储了新好友QQ
    /// </summary>
    public class FriendAddEvent : Base.NoticeEvent {
        /// <summary></summary>
        public override string notice_type { get; } = "friend_add";
        /// <summary></summary>
        public FriendAddEvent (long time, long user_id) :
            base (time, user_id) { }
    }
}