using cqhttp.Cyan.Events.CQEvents.Base;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>群成员增加或减少</summary>
    public class GroupMemberChangeEvent : GroupNoticeEvent {
        /// <summary>
        /// 退出群/加入群的方式
        /// </summary>
        public string sub_type { get; set; }
        /// <summary>
        /// 操作者id
        /// </summary>
        public long operator_id { get; set; }
    }
    /// <summary>群成员增加</summary>
    [DiscriminatorValue ("group_increase")]
    public class GroupMemberIncreaseEvent : GroupMemberChangeEvent { }
    /// <summary>群成员减少</summary>
    [DiscriminatorValue ("group_decrease")]
    public class GroupMemberDecreaseEvent : GroupMemberChangeEvent { }
}