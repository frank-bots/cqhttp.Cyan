using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Events.CQResponses {
    /// <summary>群消息回复</summary>
    [Newtonsoft.Json.JsonObject]
    public class GroupMessageResponse : Base.CQReplyMessageResponse {
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty] bool at_sender;
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty] bool admin_delete;
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty] bool admin_kick;
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty] bool admin_ban;
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty] int admin_ban_duration;

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
    }
}