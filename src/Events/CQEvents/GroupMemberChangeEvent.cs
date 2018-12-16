using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>群成员增加或减少</summary>
    public class GroupMemberChangeEvent : GroupNoticeEvent {
        /// <summary>
        /// 退出群/加入群的方式
        /// </summary>
        public string sub_type { get; private set; }
        /// <summary>
        /// 操作者id
        /// </summary>
        public long operator_id { get; private set; }
        /// <summary></summary>
        public GroupMemberChangeEvent (
            long time, long group_id, long user_id, 
            long operator_id, bool isAdd, string sub_type):
            base (
                time,
                isAdd? Enums.NoticeType.group_increase:
                Enums.NoticeType.group_decrease,
                group_id,
                user_id
            ) {
                this.sub_type = sub_type;
                this.operator_id = operator_id;
            }
    }
}