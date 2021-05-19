namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>加好友邀请</summary>
    [DiscriminatorValue ("friend")]
    public class FriendAddRequestEvent : Base.RequestEvent {
        /// <summary>请求验证信息</summary>
        public string comment { get; set; }
        /// <summary>请求 flag，在调用处理请求的 API 时需要传入</summary>
        public string flag { get; set; }
    }

    /// <summary>加群邀请</summary>
    [DiscriminatorValue ("group")]
    public class GroupAddRequestEvent : Base.RequestEvent {
        /// <summary>加入的群号</summary>
        public long group_id { get; set; }
        /// <summary>请求验证信息</summary>
        public string comment { get; set; }
        /// <summary>请求 flag，在调用处理请求的 API 时需要传入</summary>
        public string flag { get; set; }
    }
}