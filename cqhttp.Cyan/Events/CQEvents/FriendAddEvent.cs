namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 加个好友如何?
    /// 
    /// 属性this.user_id中存储了请求者QQ
    /// </summary>
    public class FriendAddEvent : Base.NoticeEvent {
        /// <summary></summary>
        public FriendAddEvent () : base () { }
        /// <summary></summary>
        public FriendAddEvent (long time, long user_id):
            base (time, Enums.NoticeType.friend_add, user_id) { }
    }
}