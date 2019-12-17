using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// “事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    [Newtonsoft.Json.JsonConverter (typeof (EventConverter))]
    public abstract class CQEvent {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; private set; } = -1;
        /// <summary>
        /// 事件类型
        /// </summary>
        public PostType postType { get; private set; }
        /// <summary>
        /// 底层构造事件
        /// </summary>
        public CQEvent (long time, PostType postType) {
            this.time = time;
            this.postType = postType;
        }
    }
}