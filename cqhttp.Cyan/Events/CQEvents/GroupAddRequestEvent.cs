namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 加个群如何?
    /// 
    /// 属性this.user_id中存储了请求者QQ
    /// </summary>
    public class GroupAddRequestEvent : Base.RequestEvent {
        /// <summary>加入的群号</summary>
        public long group_id { get; private set; }
        /// <summary>请求验证信息</summary>
        public string comment { get; private set; }
        /// <summary>请求 flag，在调用处理请求的 API 时需要传入</summary>
        public string flag { get; private set; }
        /// <summary></summary>
        public GroupAddRequestEvent (long time, long user_id, long group_id, string comment, string flag)
        : base (time, user_id) {
            this.comment = comment;
            this.flag = flag;
            this.group_id = group_id;
        }
    }
}