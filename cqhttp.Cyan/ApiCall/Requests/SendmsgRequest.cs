using System;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.Enums;
using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary></summary>
    [JsonConverter (typeof (SendmsgConverter))]
    public class SendmsgRequest : RateLimitableRequest {
        ///
        public long target_id;
        ///
        public MessageType messageType;
        /// <summary>
        /// 消息本身
        /// </summary>
        public Messages.Message message;
        /// <summary></summary>
        public SendmsgRequest (MessageType messageType, long target_id, Messages.Message toSend, bool isRateLimited = false):
            base ("/send_msg", isRateLimited) {
                if (!isRateLimited)
                    this.response = new Results.SendmsgResult ();
                else this.response = new EmptyResult ();
                this.messageType = messageType;
                this.message = toSend;
                this.target_id = target_id;
            }
    }
    class SendmsgConverter : JsonConverter {
        public override bool CanConvert (Type objectType) {
            throw new NotImplementedException ();
        }
        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException ();
        }

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            SendmsgRequest request = value as SendmsgRequest;
            writer.WriteStartObject ();
            writer.WritePropertyName ("message_type");
            switch (request.messageType) {
            case MessageType.discuss_:
                writer.WriteValue ("discuss");
                writer.WritePropertyName ("discuss_id");
                break;
            case MessageType.group_:
                writer.WriteValue ("group");
                writer.WritePropertyName ("group_id");
                break;
            case MessageType.private_:
                writer.WriteValue ("private");
                writer.WritePropertyName ("user_id");
                break;
            default:
                throw new Exceptions.ErrorApicallException ("what?");
            }
            writer.WriteValue (request.target_id);
            writer.WritePropertyName ("message");

            if (Config.is_send_json) writer.WriteRawValue (JsonConvert.SerializeObject (request.message));
            else writer.WriteValue (request.message.ToString ());
            writer.WriteEndObject ();
        }
    }
}