using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    public class GroupAdminEvent : GroupNoticeEvent {
        /// <summary></summary>
        public override string notice_type { get; } = "group_admin";
        /// <summary>
        /// set/unset是设为管理员还是撤销管理员
        /// </summary>
        public string sub_type { get; private set; }
        /// <summary></summary>
        public GroupAdminEvent (long time, long group_id, long user_id, string sub_type)
        : base (time, group_id, user_id) { this.sub_type = sub_type; }
    }
}