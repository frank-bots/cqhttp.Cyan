using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>群成员增加或减少</summary>
    [DiscriminatorValue ("group_recall")]
    public class GroupRecallEvent : GroupNoticeEvent {
        /// <summary>
        /// 操作者id
        /// </summary>
        public long operator_id { get; set; }
    }
}