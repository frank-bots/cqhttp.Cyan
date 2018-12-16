namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// 请求消息，目前仅有好友申请与加群申请
    /// </summary>
    public class RequestEvent : CQEvent {
        /// <summary>操作者ID(发起邀请者)</summary>
        public long user_id { get; private set; }
        /// <summary></summary>
        public RequestEvent (long time, long user_id) : base (time, Enums.PostType.request) {
            this.user_id = user_id;
        }
    }
}