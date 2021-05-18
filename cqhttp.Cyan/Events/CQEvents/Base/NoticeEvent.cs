using Newtonsoft.Json;
using JsonSubTypes;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary></summary>
    [JsonConverter (typeof (JsonSubtypes), "notice_type")]
    [JsonSubtypes.KnownSubType (typeof (FriendAddEvent), "friend_add")]
    [JsonSubtypes.KnownSubType (typeof (GroupAdminEvent), "group_admin")]
    [JsonSubtypes.KnownSubType (typeof (GroupBanEvent), "group_ban")]
    [JsonSubtypes.KnownSubType (typeof (GroupMemberIncreaseEvent), "group_increase")]
    [JsonSubtypes.KnownSubType (typeof (GroupMemberDecreaseEvent), "group_decrease")]
    [JsonSubtypes.KnownSubType (typeof (GroupRecallEvent), "group_recall")]
    [JsonSubtypes.KnownSubType (typeof (GroupUploadEvent), "group_upload")]
    [JsonSubtypes.KnownSubType (typeof (NotifyEvent), "notify")]
    [JsonSubtypes.FallBackSubType (typeof (UnknownEvent))]
    public abstract class NoticeEvent : CQEvent {
        /// <summary></summary>
        public override string post_type { get; } = "notice";
        /// <summary></summary>
        public virtual string notice_type { get; }

        /// <summary></summary>
        public long user_id { get; private set; }
        /// <summary></summary>
        public NoticeEvent (long time, long user_id)
        : base (time) { this.user_id = user_id; }
    }
}