using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>私聊消息</summary>
    public class PrivateMessageEvent : MessageEvent {
        /// <summary></summary>
        public override string message_type { get; } = "private";
        /// <summary></summary>
        public long sender_id { get; private set; }
        /// <summary></summary>
        public PrivateMessageEvent (
            long time,
            Message message,
            Sender sender,
            int message_id
        ) : base (time, sender, message, message_id) {
            this.sender_id = sender.user_id;
        }
    }
}