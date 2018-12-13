using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Events.CQEvents.CQResponses {
    /// <summary>群消息回复</summary>
    public class GroupMessageResponse : Base.CQReplyMessageResponse {
        /// <summary></summary>
        public bool at_sender { get; private set; }
        /// <summary></summary>
        public bool admin_delete { get; private set; }
        /// <summary></summary>
        public bool admin_kick { get; private set; }
        /// <summary></summary>
        public bool admin_ban { get; private set; }
        /// <summary></summary>
        public int admin_ban_duration { get; private set; }

        /// <param name="reply">回复的消息</param>
        /// <param name="auto_escape">是否转义字符串</param>
        /// <param name="at_sender">是否@发送者</param>
        /// <param name="delete">是否撤回(苟管理bot)</param>
        /// <param name="kick">是否踢掉发送者(苟管理bot)</param>
        /// <param name="ban">是否禁言发送者(苟管理bot)</param>
        /// <param name="ban_duration">禁言的话禁言多久(苟管理bot)</param>
        /// <returns></returns>
        public GroupMessageResponse (
            Message reply,
            bool auto_escape,
            bool at_sender = true,
            bool delete = false,
            bool kick = false,
            bool ban = false,
            int ban_duration = 0
        ) : base (reply, auto_escape) {
            this.at_sender = at_sender;
            this.admin_delete = delete;
            this.admin_kick = kick;
            this.admin_ban = ban;
            this.admin_ban_duration = ban_duration;
        }
        /// <summary></summary>
        public override string content {
            get {
                return "{" +
                    $"\"reply\":{reply}," +
                    $",\"auto_escape\":{auto_escape}" +
                    (at_sender?$",\"at_sender\":{at_sender}": "") +
                    (admin_delete?$",\"delete\":{admin_delete}": "") +
                    (admin_kick?$",\"kick\":{admin_kick}": "") +
                    (admin_ban?$",\"ban\":{admin_ban}": "") +
                    (admin_ban?$",\"ban_duration\":{admin_ban_duration}": "") +
                    "}";
            }
        }
    }
}