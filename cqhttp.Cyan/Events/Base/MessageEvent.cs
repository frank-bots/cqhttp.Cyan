using cqhttp.Cyan.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.Base {
    public enum MessageType {
        _private,
        _group,
        _discuss
    }
    public class MessageEvent : Event {

        public MessageType messageType { get; private set; }
        public Sender sender { get; private set; }
        public Message message { get; private set; }
        public MessageEvent () : base () { }
        public MessageEvent (
                int time,
                MessageType messageType,
                Sender sender,
                Message message
            ):
            base (time, PostType.message) {
                this.messageType = messageType;
                this.sender = sender;
                this.message = message;
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