using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// “事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (EventDiscriminatorOptions)
    )]
    public abstract class CQEvent {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; set; } = -1;
        /// <summary></summary>
        public string post_type { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public PostType postType {
            get {
                switch (post_type) {
                case "request": return PostType.request;
                case "message": return PostType.message;
                case "notice": return PostType.notice;
                case "meta_event": return PostType.meta_event;
                default: throw new Exceptions.ErrorEventException ("invalid post_type");
                }
            }
            set {
                post_type = value.ToString ();
            }
        }
        ///
        public CQEvent () { }
        /// <summary>
        /// 底层构造事件
        /// </summary>
        public CQEvent (long time, PostType postType) {
            this.time = time;
            this.postType = postType;
        }
    }
}