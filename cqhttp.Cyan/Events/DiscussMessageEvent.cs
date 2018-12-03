using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    public class DiscussMessageEvent : MessageEvent {
        public DiscussMessageEvent (int time, Message message, Sender sender):
            base (time, MessageType._group, sender, message) { }
        public DiscussMessageEvent () : base () { }
    }
}