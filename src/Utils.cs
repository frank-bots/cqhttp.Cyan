using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 一些常用的工具
    /// </summary>
    public class Convert {
        //善用代码折叠
        static string[] emojiDict = {
            "😲",
            "😳",
            "😍",
            "😧",
            "😏",
            "😭",
            "😇",
            "🤐",
            "💤",
            "🥺",
            "😰",
            "😡",
            "😜",
            "😬",
            "🙂",
            "🙁",
            "😦",
            "😤",
            "🤮",
            "捂嘴笑",
            "😊",
            "🤔",
            "🤨",
            "😐",
            "😴"
        };
        /// <summary>
        /// 将QQ表情转换为emoji
        /// </summary>
        /// <param name="faceId">表情编号(1-170)</param>
        public static string ToEmoji (int faceId) {
            if (faceId < emojiDict.Length)
                return emojiDict[faceId];
            else return "未知表情";
        }
        /// <summary></summary>
        public static string ToEmoji (ElementFace i) {
            return ToEmoji (int.Parse (i.data["id"]));
        }
    }
    /// <summary>
    /// 记录发送的消息
    /// </summary>
    public class MessageTable {
        List < (long, Message) > messageList = new List < (long, Message) > ();
        /// <summary>对消息进行记录以便后续撤回</summary>
        public void Log (long mid, Message event_) {
            messageList.Add ((mid, event_));
            Task.Run (() => {
                Thread.Sleep (120000);
                try { messageList.Remove ((mid, event_)); } catch { }
            });
        }
        /// <summary></summary>
        public long GetMessageId (string pattern) {
            Regex pat = new Regex (pattern);
            foreach (var i in messageList) {
                if (pat.IsMatch (i.Item2.raw_data_cq)) {
                    return i.Item1;
                }
            }
            throw new Exceptions.NullEventException ("未发现满足条件的消息");
        }
    }
    /// <summary>群成员信息</summary>
    [JsonObject]
    public class GroupMemberInfo {
        ///
        [JsonProperty ("group_id")]
        public long group_id;
        ///
        [JsonProperty ("user_id")]
        public long user_id;
        /// <summary>QQ昵称</summary>
        [JsonProperty ("nickname")]
        public string nickname;
        /// <summary>群名片</summary>
        [JsonProperty ("card")]
        public string card;
        ///
        [JsonProperty ("sex")]
        public string sex;
        ///
        [JsonProperty ("age")]
        public int age;
        ///
        [JsonProperty ("area")]
        public string area;
        ///
        [JsonProperty ("join_time")]
        public int join_time;
        ///
        [JsonProperty ("last_sent_time")]
        public int last_sent_time;
        /// <summary>成员等级</summary>
        [JsonProperty ("level")]
        public string level;
        ///
        [JsonProperty ("role")]
        public string role;
        /// <summary>是否有不良记录</summary>
        [JsonProperty ("unfriendly")]
        public bool unfriendly;
        /// <summary>专属头衔</summary>
        [JsonProperty ("title")]
        public string title;
        ///
        [JsonProperty ("title_expire_time")]
        public int title_expire_time;
        ///
        [JsonProperty ("card_changeable")]
        public bool card_changeable;
    }

    ///
    public class GroupTable : IEnumerable {
        ///
        public class GroupInfo {
            ///
            public string group_name;
            ///
            public Dictionary<long, GroupMemberInfo> group_member;

            /// <summary>附加成员</summary>
            [Obsolete ("请使用this[long user_id]", true)]
            public void Add (
                GroupMemberInfo member
            ) {
                try {
                    if (group_member.ContainsKey (member.user_id)) {
                        group_member[member.user_id] = member;
                    } else group_member.Add (
                        member.user_id,
                        member
                    );
                } catch (ArgumentNullException) {
                    //supress exceptions here
                    // 必要么？
                }
            }
            ///
            public GroupMemberInfo this [long user_id] {
                get {
                    if (group_member.ContainsKey (user_id))
                        return group_member[user_id];
                    else return null;
                }
                set {
                    if (group_member.ContainsKey (user_id))
                        group_member[user_id] = value;
                    else group_member.Add (user_id, value);
                }
            }
        }
        Dictionary<long, GroupInfo> table = new Dictionary<long, GroupInfo> ();
        ///
        public IEnumerator GetEnumerator () {
            return table.GetEnumerator ();
        }
        ///
        public GroupInfo this [long group_id] {
            get {
                if (table[group_id] == null)
                    table[group_id] = new GroupInfo ();
                return table[group_id];
            }
            set {
                if (table[group_id] == null)
                    table[group_id] = new GroupInfo ();
                table[group_id] = value;
            }
        }
    }
}