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
            long operator_id, string sub_type)
        : base (time, group_id, user_id) {
            this.sub_type = sub_type;
            this.operator_id = operator_id;
        }
    }
    /// <summary>群成员增加</summary>
    public class GroupMemberIncreaseEvent : GroupMemberChangeEvent {
        /// <summary></summary>
        public override string notice_type { get; } = "group_increase";
        /// <summary></summary>
        public GroupMemberIncreaseEvent (
            long time, long group_id, long user_id,
            long operator_id, string is_add, string sub_type)
        : base (time, group_id, user_id, operator_id, sub_type) { }
    }
    /// <summary>群成员减少</summary>
    public class GroupMemberDecreaseEvent : GroupMemberChangeEvent {
        /// <summary></summary>
        public override string notice_type { get; } = "group_decrease";
        /// <summary></summary>
        public GroupMemberDecreaseEvent (
            long time, long group_id, long user_id,
            long operator_id, string is_add, string sub_type)
        : base (time, group_id, user_id, operator_id, sub_type) { }
    }
}