using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using cqhttp.Cyan.Messages.CQElements;
using cqhttp.Cyan.Messages.CQElements.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Messages {
    class MessageConverter : JsonConverter {

        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteStartArray ();
            Message message = value as Message;
            foreach (var i in message.data)
                writer.WriteRawValue (JsonConvert.SerializeObject (i));
            writer.WriteEndArray ();
        }

        private static Element BuildCQElement (string cqcode) {
            Log.Debug ($"正在为CQ码{cqcode}构筑消息段");
            Dictionary<string, string> dict = new Dictionary<string, string> ();
            string type = Patterns.parseCqCode.Match (cqcode).Groups[1].Value;
            foreach (Match i in Patterns.paramCqCode.Matches (cqcode))
                dict.Add (i.Groups[1].Value, Encoder.Decode (i.Groups[2].Value));
            return Element.GetElement (type, dict);
        }
        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            List<Element> data = new List<Element> ();
            if (reader.TokenType == JsonToken.String) {
                Log.Debug ("收到的消息为string格式");
                string message = (string) reader.Value;
                Match match = Patterns.matchCqCode.Match (message);
                while (match.Success) {
                    if (match.Index > 0)
                        data.Add (new ElementText (
                            message.Substring (0, match.Index)
                        ));
                    data.Add (BuildCQElement (match.Value));
                    message = message.Substring (match.Index + match.Length);
                    match = Patterns.matchCqCode.Match (message);
                }
                if (message.Length != 0)
                    data.Add (new ElementText (message));
            } else if (reader.TokenType == JsonToken.StartArray) {
                Log.Debug ("收到的消息为json格式");
                while (reader.Read () && reader.TokenType != JsonToken.EndArray) {
                    JToken e = JToken.Load (reader);
                    data.Add (Element.GetElement (
                        e["type"].ToObject<string> (),
                        e["data"].ToObject<Dictionary<string, string>> ()
                    ));
                }
            } else {
                Log.Error ("type error, maybe caused by broken packet");
            }
            return new Message { data = data };

        }

        public override bool CanConvert (Type objectType) {
            return objectType == typeof (Message);
        }
    }

}