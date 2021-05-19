using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary></summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (MessageEventDiscriminatorOptions)
    )]
    public abstract class MessageEvent : CQEvent {
        /// <summary></summary>
        public MessageType messageType { get; set; }
        /// <summary>消息发送者</summary>
        public Sender sender { get; set; }
        /// <summary></summary>
        public Message message { get; set; }
        /// <summary></summary>
        public int message_id { get; set; }
        /// <summary>字体</summary>
        public int font { get; set; }

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
    public class Sender {
        /// <summary>QQ号</summary>
        public long user_id { get; set; }
        /// <summary>QQ昵称</summary>
        public string nickname { get; set; }
        /// <summary></summary>

        public string sex { get; set; }
        /// <summary></summary>

        public int age { get; set; }
    }
}