using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    public class GroupMessageEvent : MessageEvent {
        public GroupMessageEvent (int time, Message message, Sender sender):
            base (time, MessageType._group, sender, message) { }
        public GroupMessageEvent () : base () { }
    }
}