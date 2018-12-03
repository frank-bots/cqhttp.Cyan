using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    public class PrivateMessageEvent : MessageEvent {
        public PrivateMessageEvent (int time, Message message, Sender sender):
            base (time, MessageType._private, sender, message) { }
        public PrivateMessageEvent () : base () { }
    }
}