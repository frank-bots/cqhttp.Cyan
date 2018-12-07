using cqhttp.Cyan.Api.Requests.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Api.Requests {
    public class ApiSendmsg : ApiRequest {
        long target_id;
        MessageType messageType;
        Messages.Message toSend;
        public ApiSendmsg () : base () { }
        public ApiSendmsg (MessageType messageType, long target_id, Messages.Message toSend):
            base ("/send_msg") {
                this.messageType = messageType;
                this.toSend = toSend;
                this.target_id = target_id;
            }
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
                if (messageType.Length == 0) throw new ErrorApicallException ("what?");
                string constructer =
                    $"{{\"message_type\":\"{messageType}\","+
                    $"\"{idKey}\":{this.target_id},"+
                    $"\"message\":{Message.Serialize(toSend,Config.isSendJson)}}}";

                return constructer;
            }
        }
    }
}