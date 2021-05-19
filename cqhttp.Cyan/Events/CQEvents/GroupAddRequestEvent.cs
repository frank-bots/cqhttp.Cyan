namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 加个群如何?
    /// 
    /// 属性this.user_id中存储了请求者QQ
    /// </summary>
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