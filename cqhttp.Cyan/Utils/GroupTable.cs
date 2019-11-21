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
        public long group_id;
        ///
        public long user_id;
        /// <summary>QQ昵称</summary>
        public string nickname;
        /// <summary>群名片</summary>
        public string card;
        ///
        public string sex;
        ///
        public int age;
        ///
        public string area;
        ///
        public int join_time;
        ///
        public int last_sent_time;
        /// <summary>成员等级</summary>
        public string level;
        ///
        public string role;
        /// <summary>是否有不良记录</summary>
        public bool unfriendly;
        /// <summary>专属头衔</summary>
        public string title;
        ///
        public int title_expire_time;
        ///
        public bool card_changeable;
    }

    ///
    public class GroupTable : IEnumerable {
        ///
        public class GroupInfo {
            ///
            public string group_name;
            ///
            public Dictionary<long, GroupMemberInfo> group_member =
                new Dictionary<long, GroupMemberInfo> ();

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
                if (!table.ContainsKey (group_id))
                    table[group_id] = new GroupInfo ();
                return table[group_id];
            }
            set {
                if (!table.ContainsKey (group_id))
                    table[group_id] = new GroupInfo ();
                table[group_id] = value;
            }
        }
    }
}