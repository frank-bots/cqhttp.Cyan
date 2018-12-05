using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    public class PrivateMessageEvent : MessageEvent {
        public PrivateMessageEvent (
                long time,
                Message message,
                Sender sender,
                int message_id
            ):
            base (time, MessageType._private, sender, message, message_id) { }
        public PrivateMessageEvent () : base () { }
    }
}