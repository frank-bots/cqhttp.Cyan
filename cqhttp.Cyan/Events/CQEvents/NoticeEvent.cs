namespace cqhttp.Cyan.Events.CQEvents {
    #region notify event
    /// <summary></summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (Utils.DiscriminatedJsonConverter),
        typeof (NotifyNoticeEventDiscriminatorOptions)
    )]
    [DiscriminatorValue ("notify")]
    public class NotifyEvent : Base.NoticeEvent {
        /// <summary>提示类型</summary>
        public string sub_type { get; set; }
        /// <summary>被操作者 QQ 号</summary>
        public long target_id { get; set; }
    }

    /// <summary>群内戳一戳</summary>
    [DiscriminatorValue ("poke")]
    public class PokeEvent : NotifyEvent {
        ///
        public bool is_group { get => group_id != null; }
        /// <summary>群聊id，可能为null。若为null，则为私聊戳一戳。可由is_group属性判定</summary>
        public long? group_id { get; set; }
    }

    /// <summary>群红包运气王</summary>
    [DiscriminatorValue ("lucky_king")]
    public class GroupLuckyKingEvent : NotifyEvent {
        /// <summary>群聊id</summary>
        public long group_id { get; set; }
    }

    /// <summary>群成员荣誉变更</summary>
    [DiscriminatorValue ("honor")]
    public class GroupHonorEvent : NotifyEvent {
        /// <summary>荣誉类型，分别表示龙王、群聊之火、快乐源泉</summary>
        public string honor_type { get; set; }
        /// <summary>群聊id</summary>
        public long group_id { get; set; }
    }
    #endregion

    /// <summary>加好友邀请</summary>
    [DiscriminatorValue ("friend_add")]
    public class FriendAddEvent : Base.NoticeEvent { }

    /// <summary>群消息撤回</summary>
    [DiscriminatorValue ("group_recall")]
    public class GroupRecallEvent : Base.GroupNoticeEvent {
        /// <summary>撤回者id</summary>
        public long operator_id { get; set; }
    }

    /// <summary>群员被禁言</summary>
    [DiscriminatorValue ("group_ban")]
    public class GroupBanEvent : Base.GroupNoticeEvent {
        /// <summary>禁言时长</summary>
        public long duration;
    }

    /// <summary>群成员增加或减少</summary>
    public class GroupMemberChangeEvent : Base.GroupNoticeEvent {
        /// <summary>退出群/加入群的方式</summary>
        public string sub_type { get; set; }
        /// <summary>操作者id</summary>
        public long operator_id { get; set; }
    }

    /// <summary>群成员增加</summary>
    [DiscriminatorValue ("group_increase")]
    public class GroupMemberIncreaseEvent : GroupMemberChangeEvent { }

    /// <summary>群成员减少</summary>
    [DiscriminatorValue ("group_decrease")]
    public class GroupMemberDecreaseEvent : GroupMemberChangeEvent { }

    /// <summary>群管理员变更</summary>
    [DiscriminatorValue ("group_admin")]
    public class GroupAdminEvent : Base.GroupNoticeEvent {
        ///
        public string sub_type { get; set; }

        /// <summary>
        /// set/unset是设为管理员还是撤销管理员
        /// </summary>
        public bool is_set { get => sub_type == "set"; }
    }

    /// <summary>
    /// <see>https://cqhttp.cc/docs/4.6/#/Post?id=%E7%BE%A4%E6%96%87%E4%BB%B6%E4%B8%8A%E4%BC%A0</see>
    /// </summary>
    [DiscriminatorValue ("group_upload")]
    public class GroupUploadEvent : Base.GroupNoticeEvent {
        /// <summary></summary>
        public class FileInfo {
            /// <summary></summary>
            public string id { get; set; }
            /// <summary>文件名</summary>
            public string name { get; set; }
            /// <summary></summary>
            public long size { get; set; }
            /// <summary>cqhttp作者也不知道，我也不知道是干啥的(总线id？那是啥)</summary>
            public long busid { get; set; }
        }
        /// <summary>上传文件信息</summary>
        public FileInfo fileInfo { get; set; }
    }
}