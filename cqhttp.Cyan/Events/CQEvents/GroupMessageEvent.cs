using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>收到群消息</summary>
    public class GroupMessageEvent : MessageEvent {
        /// <summary></summary>
        public override string message_type { get; } = "group";
        /// <summary></summary>
        public long group_id { get; private set; }
        /// <summary></summary>
        public string sub_type { get; private set; }
        /// <summary>群消息是否匿名</summary>
        public bool isAnonymous {
            get {
                return sub_type == "anonymous";
            }
        }
        /// <summary>
        /// 表示机器人在消息所在群中的身份信息
        /// 来自于groupTable缓存或GetGroupMemberList结果
        /// </summary>
        public Utils.GroupMemberInfo self_info = null;
        /// <summary></summary>
        public GroupMessageEvent (
            long time,
            string sub_type,
            Message message,
            GroupSender sender,
            int message_id,
            long group_id,
            AnonymousInfo anonymous
        ) : base (time, sender, message, message_id) {
            this.group_id = group_id;
            this.sub_type = sub_type;
        }
    }

    ///
    [JsonObject]
    public class AnonymousInfo {
        /// <summary>匿名用户id</summary>
        public long id { get; private set; }
        /// <summary>匿名用户名称</summary>
        public string name { get; private set; }
        /// <summary>匿名用户 flag，在调用禁言 API 时需要传入</summary>
        public string flag { get; private set; }
    }
    /// <summary>
    /// 群消息发送者信息，匿名时无参考价值
    /// </summary>
    [JsonObject]
    public class GroupSender : Sender {
        /// <summary>来自</summary>
        [JsonProperty ("area")] public string area { get; private set; }

        /// <summary>成员等级</summary>
        [JsonProperty ("level")] public string level { get; private set; }

        /// <summary>身份(owner/admin/member)</summary>
        [JsonProperty ("role")] public string role { get; private set; }
        /// <summary>头衔</summary>
        [JsonProperty ("title")] public string title { get; private set; }
        /// <summary>群名片／备注</summary>
        [JsonProperty ("card")] public string card { get; private set; }
    }
}