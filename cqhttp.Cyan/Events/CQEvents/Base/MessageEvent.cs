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
        /// 获取消息的发送位置
        /// </summary>
        /// <returns>返回一个Tuple，Item1代表对话种类（群聊/私聊/讨论组），Item2代表号码（群号/QQ号/讨论组号）</returns>
        public (MessageType, long) GetEndpoint () {
            switch (this) {
            case GroupMessageEvent g:
                return (MessageType.group_, g.group_id);
            case PrivateMessageEvent p:
                return (MessageType.private_, p.sender_id);
            case DiscussMessageEvent d:
                return (MessageType.discuss_, d.discuss_id);
            default:
                return (MessageType.private_, 745679136);
            }
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