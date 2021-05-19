using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>群成员增加或减少</summary>
    public class GroupRecallEvent : GroupNoticeEvent {
        /// <summary>
        /// 操作者id
        /// </summary>
        public long operator_id { get; private set; }
        /// <summary></summary>
        public GroupRecallEvent (
            long time, long group_id, long user_id, long operator_id
        ) : base (
            time,
            Enums.NoticeType.group_recall,
            group_id, user_id
        ) {
            this.operator_id = operator_id;
        }
    }
}