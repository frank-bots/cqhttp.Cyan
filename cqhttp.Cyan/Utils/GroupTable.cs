using System.Collections.Generic;
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
        public ulong join_time;
        ///
        public ulong last_sent_time;
        /// <summary>成员等级</summary>
        public string level;
        ///
        public string role;
        /// <summary>是否有不良记录</summary>
        public bool unfriendly;
        /// <summary>专属头衔</summary>
        public string title;
        ///
        public ulong title_expire_time;
        ///
        public bool card_changeable;
    }

    ///
    public class GroupTable : Dictionary<long, Dictionary<long, GroupMemberInfo>> {
        ///
        public new Dictionary<long, GroupMemberInfo> this [long group_id] {
            get {
                if (!ContainsKey (group_id))
                    Add (group_id, new Dictionary<long, GroupMemberInfo> ());
                return this.GetValueOrDefault (group_id, null);
            }
            set {
                this [group_id] = value;
            }
        }
    }
}