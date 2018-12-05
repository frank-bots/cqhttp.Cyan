using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    public class GroupMessageEvent : MessageEvent {
        long group_id;
        public GroupMessageEvent (
                long time,
                Message message,
                Sender sender,
                int message_id,
                long group_id
            ):
            base (time, MessageType._group, sender, message, message_id) {
                this.group_id = group_id;
            }
        public GroupMessageEvent () : base () { }
    }
}