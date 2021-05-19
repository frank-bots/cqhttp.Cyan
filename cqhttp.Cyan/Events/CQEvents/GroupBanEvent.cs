using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 群员被禁言
    /// </summary>
    [DiscriminatorValue ("group_ban")]
    public class GroupBanEvent : GroupNoticeEvent {
        /// <summary>
        /// 禁言时长
        /// </summary>
        public long duration;
    }
}