using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 群员被禁言
    /// </summary>
    public class GroupBanEvent : GroupNoticeEvent {
        /// <summary>
        /// 禁言时长
        /// </summary>
        public long duration;
        ///
        public GroupBanEvent (
                long time, bool is_ban,
                long group_id, long operator_id, long user_id,
                long duration):
            base (time, NoticeType.friend_add, group_id, user_id) {
                this.duration = duration;
            }
    }
}