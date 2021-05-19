using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary></summary>
    [DiscriminatorValue ("group_admin")]
    public class GroupAdminEvent : GroupNoticeEvent {
        ///
        public string sub_type { get; set; }

        /// <summary>
        /// set/unset是设为管理员还是撤销管理员
        /// </summary>
        public bool is_set { get => sub_type == "set"; }
    }
}