namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 属性this.user_id中存储了新好友QQ
    /// </summary>
    public class FriendAddEvent : Base.NoticeEvent {
        /// <summary></summary>
        public FriendAddEvent (long time, long user_id)
        : base (time, Enums.NoticeType.friend_add, user_id) { }
    }
}