using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    [DiscriminatorValue ("private")]
    public class PrivateMessageEvent : MessageEvent {
        /// <summary></summary>
        public long sender_id { get => sender.user_id; }
    }
}