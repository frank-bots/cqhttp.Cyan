using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.CQEvents {
    public class PrivateMessageEvent : MessageEvent {
        public PrivateMessageEvent (
                long time,
                Message message,
                Sender sender,
                int message_id
            ):
            base (time, Enums.MessageType.private_, sender, message, message_id) { }
        public PrivateMessageEvent () : base () { }
    }
}