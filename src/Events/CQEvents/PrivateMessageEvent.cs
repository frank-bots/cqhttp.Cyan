using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    public class PrivateMessageEvent : MessageEvent {
        /// <summary></summary>
        public long sender_id { get; private set; }
        /// <summary></summary>
        public PrivateMessageEvent (
                long time,
                Message message,
                Sender sender,
                int message_id
            ):
            base (time, Enums.MessageType.private_, sender, message, message_id) {
                this.sender_id = sender.user_id;
            }
        /// <summary></summary>
        public PrivateMessageEvent () : base () { }
    }
}