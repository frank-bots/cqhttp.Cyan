using cqhttp.Cyan.Events.CQEvents.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// <see>https://cqhttp.cc/docs/4.6/#/Post?id=%E7%BE%A4%E6%96%87%E4%BB%B6%E4%B8%8A%E4%BC%A0</see>
    /// </summary>
    public class NotifyEvent : GroupNoticeEvent {
        /// <summary></summary>
        public override string notice_type { get; } = "notify";
        /// <summary>被操作者QQ号</summary>
        public long target_id { get; private set; }
        /// <summary>通知类型</summary>
        public string sub_type { get; private set; }
        /// <summary></summary>
        public NotifyEvent (long time, string sub_type, long group_id, long user_id, long target_id)
        : base (time, group_id, user_id) {
            this.sub_type = sub_type;
            this.target_id = target_id;
        }
    }
}