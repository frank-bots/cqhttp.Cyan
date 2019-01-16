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