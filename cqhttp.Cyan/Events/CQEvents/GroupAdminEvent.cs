using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    public class GroupAdminEvent : GroupNoticeEvent {
        /// <summary>
        /// set/unset是设为管理员还是撤销管理员
        /// </summary>
        public bool isSet { get; private set; }
        /// <summary></summary>
        public GroupAdminEvent (long time, long group_id, long user_id, bool isSet)
        : base (time, Enums.NoticeType.group_admin, group_id, user_id) {
            this.isSet = isSet;
        }
    }
}