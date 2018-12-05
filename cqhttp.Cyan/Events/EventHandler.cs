using System;
using cqhttp.Cyan;
using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    public class CQEventHandler {
        public static CQEvent Handle (string e) {
            string post_type;
            JObject eventJson;
            try {
                eventJson = JObject.Parse (e);
                post_type = eventJson["post_type"].ToString ();
            } catch (JsonException) {
                throw new ErrorEventException ("收到了错误的上报消息");
            }
            switch (post_type) {
                case "message":
                    return HandleMessage (ref eventJson);
                case "notice":
                    return HandleNotice (ref eventJson);
                case "request":
                    return HandleRequest (ref eventJson);
                case "meta_event":
                    break;
            }
            throw new NullEventException ($"未能解析type为{post_type}的event");
        }

        public static CQEvent HandleMessage (ref JObject e) {
            string sub_type = e["message_type"].ToString ();
            short temp;
            switch (sub_type) {
                case "private":
                    return new PrivateMessageEvent (
                        e["time"].ToObject<long> (),
                        Message.Deserialize (e["message"].ToString (), out temp),
                        e["sender"].ToObject<Sender> (),
                        e["message_id"].ToObject<int> ()
                    );
                case "group":
                    return new GroupMessageEvent (
                        e["time"].ToObject<long> (),
                        Message.Deserialize (e["message"].ToString (), out temp),
                        e["sender"].ToObject<Sender> (),
                        e["message_id"].ToObject<int> (),
                        e["group_id"].ToObject<long> ()
                    );
                case "discuss":
                    return new DiscussMessageEvent (
                        e["time"].ToObject<long> (),
                        Message.Deserialize (e["message"].ToString (), out temp),
                        e["sender"].ToObject<Sender> (),
                        e["message_id"].ToObject<int> (),
                        e["discuss_id"].ToObject<long> ()
                    );
            }
            throw new ErrorEventException ("未能解析消息事件");
        }
        private static CQEvent HandleRequest (ref JObject eventJson) {
            throw new NotImplementedException ();
        }

        private static CQEvent HandleNotice (ref JObject eventJson) {
            throw new NotImplementedException ();
        }

    }

}