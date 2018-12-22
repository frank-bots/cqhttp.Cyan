using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages.CQElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        Queue<MessageEvent> queue = new Queue<MessageEvent> ();
        Task monitor;
        CancellationTokenSource monitorCanceller = new CancellationTokenSource ();
        /// <summary></summary>
        ~MessageTable () {
            monitorCanceller.Cancel ();
        }
        /// <summary></summary>
        public MessageTable () {
            monitor = Task.Run (() => {
                while (true) {
                    Thread.Sleep (1000);
                    if (queue.Count > 0) {
                        if (queue.Peek ().time - DateTime.Now.ToFileTime () > 2 * 1)
                            queue.Dequeue ();

                    }
                }
            }, monitorCanceller.Token);
        }
        /// <summary>对消息进行记录以便后续撤回</summary>
        public void Log (MessageEvent event_) =>
            queue.Enqueue (event_);
        /// <summary></summary>
        public void GetMessageId (string pattern) {
            //不知道怎么写好
        }
    }
    /// <summary>群成员信息</summary>
    [JsonObject]
    public class GroupMemberInfo {
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
            public static GroupInfo operator + (
                GroupInfo groupInfo, JToken member
            ) {
                groupInfo.group_member.Add (
                    member["user_id"].ToObject<long> (),
                    member.ToObject<GroupMemberInfo> ()
                );
                return groupInfo;
            }
        }
        Dictionary<long, GroupInfo> table;
        ///
        public IEnumerator GetEnumerator () {
            throw new NotImplementedException ();
        }
        ///
        public object this [long i] {
            get {
                if (table[i] == null)
                    table[i] = new GroupInfo ();
                return table[i];
            }
            set {
                if (table[i] == null)
                    table[i] = new GroupInfo ();
                if (value is string)
                    table[i].group_name = (value as string);
                else if (value is Dictionary<long, GroupMemberInfo>)
                    table[i].group_member = value as Dictionary<long, GroupMemberInfo>;
                else throw new Exceptions.ErrorUtilOperationException ("待描述");
                //TODO: 描述这个不可描述的错误
            }
        }
    }
}