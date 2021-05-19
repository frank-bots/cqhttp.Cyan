namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 加个好友如何?
    /// 
    /// 属性this.user_id中存储了请求者QQ
    /// </summary>
    public class FriendAddRequestEvent : Base.RequestEvent {
        /// <summary>请求验证信息</summary>
        public string comment { get; private set; }
        /// <summary>请求 flag，在调用处理请求的 API 时需要传入</summary>
        public string flag { get; private set; }
        /// <summary></summary>
        public FriendAddRequestEvent (long time, long user_id, string comment, string flag)
        : base (time, user_id) {
            this.comment = comment;
            this.flag = flag;
        }
    }
}