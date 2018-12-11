using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>收到群消息</summary>
    public class GroupMessageEvent : MessageEvent {
        long group_id;
        /// <summary></summary>
        public GroupMessageEvent (
                long time,
                Message message,
                Sender sender,
                int message_id,
                long group_id
            ):
            base (time, Enums.MessageType.group_, sender, message, message_id) {
                this.group_id = group_id;
            }
        /// <summary></summary>
        public GroupMessageEvent () : base () { }
    }
}