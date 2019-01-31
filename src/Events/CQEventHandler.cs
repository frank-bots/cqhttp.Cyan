using System;
using cqhttp.Cyan;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events {
    /// <summary>
    /// 事件处理
    /// </summary>
    public class CQEventHandler {
        /// <summary>
        /// 处理完整的上报事件
        /// </summary>
        /// <param name="e">上报事件</param>
        /// <returns>处理后的事件对象</returns>
        public static CQEvent HandleEvent (string e) {
            string post_type;
            JObject eventJson;
            try {
                eventJson = JObject.Parse (e);
            } catch (JsonException) {
                Logger.Log (Verbosity.ERROR, $"收到了错误的上报消息{e}");
                throw new Exceptions.ErrorEventException ("收到了错误的上报消息");
            }
            post_type = eventJson["post_type"].ToString ();
            switch (post_type) {
                case "message":
                    return HandleMessage (ref eventJson);
                case "notice":
                    return HandleNotice (ref eventJson);
                case "request":
                    return HandleRequest (ref eventJson);
                case "meta_event":
                    switch (eventJson["meta_event_type"].ToString ()) {
                        case "lifecycle":
                            return new MetaEvents.LifecycleEvent (
                                eventJson["time"].ToObject<long> (),
                                eventJson["sub_type"].ToString () == "enable"
                            );
                        case "heartbeat":
                            return new MetaEvents.HeartbeatEvent (
                                eventJson["time"].ToObject<long> (),
                                eventJson["status"].ToObject<Status> ()
                            );
                        default:
                            Logger.Log (
                                Verbosity.ERROR,
                                $"未能解析元事件{eventJson.ToString()}"
                            );
                            throw new Exceptions.ErrorEventException ("未能解析元事件");
                    }
            }
            Logger.Log (Verbosity.ERROR, $"未能解析type为{post_type}的event");
            throw new Exceptions.NullEventException ($"未能解析type为{post_type}的event");
        }

        private static CQEvent HandleMessage (ref JObject e) {
            string sub_type = e["message_type"].ToString ();
            switch (sub_type) {
                case "private":
                    return new PrivateMessageEvent (
                        e["time"].ToObject<long> (),
                        Message.Parse (e["message"].ToString ()),
                        e["sender"].ToObject<Sender> (),
                        e["message_id"].ToObject<int> ()
                    );
                case "group":
                    return new GroupMessageEvent (
                        e["time"].ToObject<long> (),
                        e["sub_type"].ToString (),
                        Message.Parse (e["message"].ToString ()),
                        e["sender"].ToObject<GroupSender> (),
                        e["message_id"].ToObject<int> (),
                        e["group_id"].ToObject<long> ()
                    );
                case "discuss":
                    return new DiscussMessageEvent (
                        e["time"].ToObject<long> (),
                        Message.Parse (e["message"].ToString ()),
                        e["sender"].ToObject<Sender> (),
                        e["message_id"].ToObject<int> (),
                        e["discuss_id"].ToObject<long> ()
                    );
                default:
                    Logger.Log (
                        Verbosity.ERROR,
                        $"未能解析消息事件{e.ToString()}"
                    );
                    return new UnknownEvent (
                        e["time"].ToObject<long> (),
                        PostType.message,
                        e.ToString ()
                    );
            }
            //throw new Exceptions.ErrorEventException ("未能解析消息(message)事件");
        }
        private static CQEvent HandleRequest (ref JObject e) {
            string request_type = e["request_type"].ToString ();
            switch (request_type) {
                case "friend":
                    return new FriendAddRequestEvent (
                        e["time"].ToObject<long> (),
                        e["user_id"].ToObject<long> (),
                        e["comment"].ToString (),
                        e["flag"].ToString ()
                    );
                case "group":
                    return new GroupAddRequestEvent (
                        e["time"].ToObject<long> (),
                        e["user_id"].ToObject<long> (),
                        e["group_id"].ToObject<long> (),
                        e["comment"].ToString (),
                        e["flag"].ToString ()
                    );
                default:
                    Logger.Log (
                        Verbosity.ERROR,
                        $"未能解析请求事件{e.ToString()}"
                    );
                    return new UnknownEvent (
                        e["time"].ToObject<long> (),
                        PostType.request,
                        e.ToString ()
                    );
            }
            //throw new Exceptions.ErrorEventException ("未能解析请求(request)事件");
        }

        private static CQEvent HandleNotice (ref JObject e) {
            string notice_type = e["notice_type"].ToString ();
            switch (notice_type) {
                case "group_upload":
                    return new GroupUploadEvent (
                        e["time"].ToObject<long> (),
                        e["file"].ToObject<FileInfo> (),
                        e["group_id"].ToObject<long> (),
                        e["user_id"].ToObject<long> ()
                    );
                case "group_admin":
                    return new GroupAdminEvent (
                        e["time"].ToObject<long> (),
                        e["group_id"].ToObject<long> (),
                        e["user_id"].ToObject<long> (),
                        e["sub_type"].ToString () == "set"
                    );
                case "group_decrease":
                case "group_increase":
                    return new GroupMemberChangeEvent (
                        e["time"].ToObject<long> (),
                        e["group_id"].ToObject<long> (),
                        e["user_id"].ToObject<long> (),
                        e["operator_id"].ToObject<long> (),
                        notice_type == "group_increase",
                        e["sub_type"].ToString ()
                    );
                case "friend_add":
                    return new FriendAddEvent (
                        e["time"].ToObject<long> (),
                        e["user_id"].ToObject<long> ()
                    );
                default:
                    Logger.Log (
                        Verbosity.ERROR,
                        $"未能解析提醒事件{e.ToString()}"
                    );
                    return new UnknownEvent (
                        e["time"].ToObject<long> (),
                        PostType.notice,
                        e.ToString ()
                    );

            }
            // throw new Exceptions.ErrorEventException ("未能解析提醒(notice)事件");
        }

    }

}