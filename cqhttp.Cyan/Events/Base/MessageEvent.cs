using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.Base {
        public class MessageEvent : CQEvent {

        public MessageType messageType { get; private set; }
        public Sender sender { get; private set; }
        public Message message { get; private set; }
        public int message_id { get; private set; }
        public int font { get; private set; }
        public MessageEvent () : base () { }
        public MessageEvent (
                long time,
                MessageType messageType,
                Sender sender,
                Message message,
                int message_id,
                int font = 0
            ):
            base (time, PostType.message) {
                this.messageType = messageType;
                this.sender = sender;
                this.message = message;
                this.message_id = message_id;
                this.font = font;
            }
    }

    [JsonObject]
    public class Sender {
        [JsonProperty ("user_id")]
        public long user_id { get; private set; }

        [JsonProperty ("nickname")]
        public string nickname { get; private set; }

        [JsonProperty ("sex")]
        public string sex { get; private set; }

        [JsonProperty ("age")]
        public int age { get; private set; }
    }
}