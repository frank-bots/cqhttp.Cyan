using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    public class DiscussMessageEvent : MessageEvent {
        /// <summary></summary>
        public override string message_type { get; } = "discuss";
        /// <summary>讨论组编号，只能从此处获取</summary>
        public long discuss_id { get; private set; }
        /// <summary></summary>
        public DiscussMessageEvent (
            long time,
            Message message,
            Sender sender,
            int message_id,
            long discuss_id
        ) : base (time, sender, message, message_id) {
            this.discuss_id = discuss_id;
        }
    }
}