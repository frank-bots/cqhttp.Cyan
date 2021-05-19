using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    [DiscriminatorValue ("discuss")]
    public class DiscussMessageEvent : MessageEvent {
        /// <summary>讨论组编号，只能从此处获取</summary>
        public long discuss_id { get; set; }
    }
}