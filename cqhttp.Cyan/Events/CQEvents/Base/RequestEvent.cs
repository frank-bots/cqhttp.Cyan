using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// 请求消息，目前仅有好友申请与加群申请
    /// </summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (RequestEventDiscriminatorOptions)
    )]
    public class RequestEvent : CQEvent {
        /// <summary></summary>
        public string request_type { get; set; }
        /// <summary>操作者ID(发起邀请者)</summary>
        public long user_id { get; set; }
    }
}