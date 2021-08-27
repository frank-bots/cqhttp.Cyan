using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary>
    /// “事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (EventDiscriminatorOptions)
    )]
    public abstract class CQEvent {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; set; } = -1;
        /// <summary></summary>
        public string post_type { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public PostType postType {
            get {
                switch (post_type) {
                case "request": return PostType.request;
                case "message": return PostType.message;
                case "notice": return PostType.notice;
                case "meta_event": return PostType.meta_event;
                default: throw new Exceptions.ErrorEventException ("invalid post_type");
                }
            }
            set {
                post_type = value.ToString ();
            }
        }
        /// <summary>
        /// 原始事件数据
        /// </summary>
        public Newtonsoft.Json.Linq.JObject raw_event { get; set; }
        ///
        public CQEvent () { }
        /// <summary>
        /// 底层构造事件
        /// </summary>
        public CQEvent (long time, PostType postType) {
            this.time = time;
            this.postType = postType;
        }
    }
    /// <summary></summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (MessageEventDiscriminatorOptions)
    )]
    public abstract class MessageEvent : CQEvent {

        /// <summary>发送者信息</summary>
        public class Sender {
            /// <summary>QQ号</summary>
            public long user_id { get; set; }
            /// <summary>QQ昵称</summary>
            public string nickname { get; set; }
            /// <summary>性别</summary>
            public string sex { get; set; }
            /// <summary>年龄</summary>
            public int age { get; set; }
        }
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
    /// <summary></summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (NoticeEventDiscriminatorOptions)
    )]
    public abstract class NoticeEvent : CQEvent {
        /// <summary></summary>
        public string notice_type { get; set; }
        /// <summary></summary>
        public NoticeType noticeType {
            get {
                foreach (var x in System.Enum.GetValues (typeof (NoticeType)))
                    if (x.ToString () == notice_type)
                        return (NoticeType) x;
                throw new Exceptions.ErrorEventException ("invalid notice_type");
            }
        }
        /// <summary></summary>
        public long user_id { get; set; }
    }
    /// <summary>
    /// 请求消息，目前仅有好友申请与加群申请
    /// </summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (RequestEventDiscriminatorOptions)
    )]
    public class RequestEvent : CQEvent {
        /// <summary></summary>
        public string request_type { get; set; }
        /// <summary>操作者ID(发起邀请者)</summary>
        public long user_id { get; set; }
    }
    /// <summary>
    /// <see>https://cqhttp.cc/docs/4.6/#/Post?id=%E4%BA%8B%E4%BB%B6%E5%88%97%E8%A1%A8</see>
    /// </summary>
    public abstract class GroupNoticeEvent : NoticeEvent {
        /// <summary>
        /// 事件发生的群号码
        /// </summary>
        public long group_id { get; set; }
    }
}