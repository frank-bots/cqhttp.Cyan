using JsonSubTypes;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// 请求消息，目前仅有好友申请与加群申请
    /// </summary>
    [JsonConverter (typeof (JsonSubtypes), "request_type")]
    [JsonSubtypes.KnownSubType (typeof (GroupAddRequestEvent), "group")]
    [JsonSubtypes.KnownSubType (typeof (FriendAddRequestEvent), "friend")]
    [JsonSubtypes.FallBackSubType (typeof (UnknownEvent))]
    public class RequestEvent : CQEvent {
        /// <summary></summary>
        public override string post_type { get; } = "request";
        /// <summary></summary>
        public virtual string request_type { get; }
        /// <summary>操作者ID(发起邀请者)</summary>
        public long user_id { get; private set; }
        /// <summary></summary>
        public RequestEvent (long time, long user_id) : base (time) {
            this.user_id = user_id;
        }
    }
}