using cqhttp.Cyan.Enums;
using JsonSubTypes;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// “事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    [Newtonsoft.Json.JsonConverter (typeof (JsonSubtypes), "post_type")]
    [JsonSubtypes.KnownSubType (typeof (MessageEvent), "message")]
    [JsonSubtypes.KnownSubType (typeof (NoticeEvent), "notice")]
    [JsonSubtypes.KnownSubType (typeof (RequestEvent), "request")]
    [JsonSubtypes.KnownSubType (typeof (MetaEvents.MetaEvent), "meta_event")]
    [JsonSubtypes.FallBackSubType (typeof (UnknownEvent))]
    public abstract class CQEvent {
        /// <summary>时间戳</summary>
        public long time { get; private set; } = -1;
        /// <summary>事件类型</summary>
        public virtual string post_type { get; }
        /// <summary>事件类型</summary>
        public PostType postType {
            get {
                switch (post_type) {
                case "message": return PostType.message;
                case "notice": return PostType.notice;
                case "request": return PostType.request;
                case "meta_event": return PostType.meta_event;
                default: throw new Exceptions.ErrorEventException ("未能解析上报类型");
                }
            }
        }
        /// <summary>底层构造事件</summary>
        public CQEvent (long time) { this.time = time; }
    }
}