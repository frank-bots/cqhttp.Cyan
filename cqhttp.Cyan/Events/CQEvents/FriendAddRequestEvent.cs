namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 加个好友如何?
    /// 
    /// 属性this.user_id中存储了请求者QQ
    /// </summary>
    [DiscriminatorValue ("friend")]
    public class FriendAddRequestEvent : Base.RequestEvent {
        /// <summary>请求验证信息</summary>
        public string comment { get; set; }
        /// <summary>请求 flag，在调用处理请求的 API 时需要传入</summary>
        public string flag { get; set; }
    }
}