using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary></summary>
    public class SendmsgRequest : RateLimitableRequest {
        long target_id;
        MessageType messageType;
        Messages.Message toSend;
        /// <summary></summary>
        public SendmsgRequest (MessageType messageType, long target_id, Messages.Message toSend, bool isRateLimited = false):
            base ("/send_msg", isRateLimited) {
                if (!isRateLimited)
                    this.response = new Results.SendmsgResult ();
                else this.response = new EmptyResult ();
                this.messageType = messageType;
                this.toSend = toSend;
                this.target_id = target_id;
            }
        /// <summary></summary>
        public override string content {
            get {
                string messageType, idKey;
                switch (this.messageType) {
                    case MessageType.discuss_:
                        messageType = "discuss";
                        idKey = "discuss_id";
                        break;
                    case MessageType.group_:
                        messageType = "group";
                        idKey = "group_id";
                        break;
                    case MessageType.private_:
                        messageType = "private";
                        idKey = "user_id";
                        break;
                    default:
                        messageType = idKey = "";
                        break;
                }
                if (messageType.Length == 0)
                    throw new Exceptions.ErrorApicallException ("what?");
                string constructer =
                    $"{{\"message_type\":\"{messageType}\","+
                    $"\"{idKey}\":{this.target_id},"+
                    $"\"message\":{toSend.ToString()}}}";

                return constructer;
            }
        }
    }
}