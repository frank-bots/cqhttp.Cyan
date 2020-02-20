using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary></summary>
    public abstract class MessageEvent : CQEvent {
        /// <summary></summary>
        public MessageType messageType { get; private set; }
        /// <summary>消息发送者</summary>
        public Sender sender { get; private set; }
        /// <summary></summary>
        public Message message { get; private set; }
        /// <summary></summary>
        public int message_id { get; private set; }
        /// <summary>字体</summary>
        public int font { get; private set; }
        /// <summary></summary>
        public MessageEvent (
            long time,
            MessageType messageType,
            Sender sender,
            Message message,
            int message_id,
            int font = 0
        ) : base (time, PostType.message) {
            this.messageType = messageType;
            this.sender = sender;
            this.message = message;
            this.message_id = message_id;
            this.font = font;
        }

        /// <summary>
        /// 获取当前的
        /// </summary>
        /// <returns></returns>
        public (MessageType, long) GetEndpoint () {
            if (this is GroupMessageEvent) {
                return (
                    MessageType.group_,
                    (this as GroupMessageEvent).group_id
                );
            } else if (this is PrivateMessageEvent) {
                return (
                    MessageType.private_,
                    (this as PrivateMessageEvent).sender_id
                );
            } else if (this is DiscussMessageEvent) {
                return (
                    MessageType.discuss_,
                    (this as DiscussMessageEvent).discuss_id
                );
            } else return (MessageType.private_, 745679136);
        }
    }
    /// <summary>
    /// 发送者信息
    /// </summary>
    [JsonObject]
    public class Sender {
        /// <summary>QQ号</summary>
        [JsonProperty ("user_id")] public long user_id { get; private set; }
        /// <summary>QQ昵称</summary>
        [JsonProperty ("nickname")] public string nickname { get; private set; }
        /// <summary></summary>

        [JsonProperty ("sex")] public string sex { get; private set; }
        /// <summary></summary>

        [JsonProperty ("age")] public int age { get; private set; }
    }
}