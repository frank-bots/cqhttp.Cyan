using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>收到群消息</summary>
    [DiscriminatorValue ("group")]
    public class GroupMessageEvent : MessageEvent {
        /// <summary></summary>
        public long group_id { get; set; }
        /// <summary></summary>
        public string sub_type { get; set; }
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
    }

    ///
    [JsonObject]
    public class AnonymousInfo {
        /// <summary>匿名用户id</summary>
        public long id { get; set; }
        /// <summary>匿名用户名称</summary>
        public string name { get; set; }
        /// <summary>匿名用户 flag，在调用禁言 API 时需要传入</summary>
        public string flag { get; set; }
    }
    /// <summary>
    /// 群消息发送者信息，匿名时无参考价值
    /// </summary>
    [JsonObject]
    public class GroupSender : Sender {
        /// <summary>来自</summary>
        public string area { get; set; }

        /// <summary>成员等级</summary>
        public string level { get; set; }

        /// <summary>身份(owner/admin/member)</summary>
        public string role { get; set; }
        /// <summary>头衔</summary>
        public string title { get; set; }
        /// <summary>群名片／备注</summary>
        public string card { get; set; }
    }
}