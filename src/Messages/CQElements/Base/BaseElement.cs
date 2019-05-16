using System.Collections.Generic;

namespace cqhttp.Cyan.Messages.CQElements.Base {
    /// <summary>
    /// 消息元素，即cqhttp所定义的消息段
    /// </summary>
    /// <remarks>
    /// should NEVER be constructed or used directly
    /// </remarks>
    public class Element {
        /// <summary>
        /// 消息段类型
        /// </summary>
        public string type { get; private set; }
        /// <summary>
        /// represents the true message 
        /// </summary>
        public virtual Dictionary<string, string> data { get; private set; }
        /// <summary>
        /// 表示消息段是否只能单独发送
        /// </summary>
        public bool isSingle = false;

        /// <summary></summary>
        public static Message operator + (Element a, Element b) {
            return new Message (a, b);
        }
        /// <summary></summary>
        public static bool operator == (Element a, Element b) {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            if (a.type != b.type) return false;
            var i = a.data.GetEnumerator ();
            var j = b.data.GetEnumerator ();
            while (i.Current.Key == j.Current.Key && i.Current.Value == j.Current.Value) {
                if (i.MoveNext ()) {
                    if (j.MoveNext ()) continue;
                    else break;
                } else {
                    j.MoveNext ();
                    break;
                }
            }
            if (!(i.Current.Key is null) || !(j.Current.Key is null)) return false;
            return true;
        }
        /// <summary></summary>
        public static bool operator != (Element a, Element b) {
            return !(a == b);
        }
        /// <summary></summary>
        public override bool Equals (object b) {
            if (b is Element) {
                return this == (b as Element);
            }
            return false;
        }
        /// <summary></summary>
        public override int GetHashCode () {
            int hash = 1;
            foreach (var i in data)
                hash ^= i.GetHashCode ();
            return hash;
        }
        /// <summary>
        /// builds the value when constructing CQCode
        /// <see>https://d.cqp.me/Pro/CQ%E7%A0%81</see>
        /// <see cref="CQElements.ElementText"/>
        /// </summary>
        /// <value>CQCode</value>
        public string raw_data_cq {
            get {
                if (type == "text")
                    return Encoder.EncodeText (data["text"]);
                string paramBuilder = "";
                foreach (var i in data)
                    if (i.Value.Length > 0)
                        paramBuilder += $",{i.Key}={Encoder.EncodeValue(i.Value)}";
                return string.Format (
                    "[CQ:{0}{1}]",
                    type, paramBuilder.TrimEnd (' ')
                );
            }
        }

        /// <summary>
        /// builds the value when constructing EXTENDED CQCode
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        /// </summary>
        public string raw_data_json {
            get {
                string builder = $"{{\"type\":\"{type}\",\"data\":{{";
                foreach (var i in data)
                    builder += $"\"{i.Key}\":\"{Config.asJsonStringVariable(i.Value)}\",";
                return builder.TrimEnd (',', ' ')+$"}}}}";
                //。。。不这么写的话我的代码格式化插件就会崩掉
            }
        }
        /// <summary>
        /// 手动构造一个消息段，一般用不到
        /// </summary>
        /// <param name="type">消息段类型</param>
        /// <param name="dict">手动输入的键值对</param>
        public Element (string type, params (string key, string value) [] dict) {
            this.type = type;
            data = new Dictionary<string, string> ();
            foreach (var i in dict)
                data.Add (i.key, i.value);
        }
        /// <summary>
        /// 构造消息段，一般不会手动调用
        /// </summary>
        /// <param name="type">消息段类型</param>
        /// <param name="dict">消息段键值对</param>
        public Element (string type, Dictionary<string, string> dict) {
            this.type = type;
            data = dict;
        }
    }
    class Encoder {
        public static string EncodeText (string enc) => enc.Replace ("&", "&amp;").Replace ("[", "&#91;").Replace ("]", "&#93;");
        public static string EncodeValue (string text) => EncodeText (text).Replace (",", "&#44;");
        public static string Decode (string enc) => enc.Replace ("&amp;", "&").Replace ("&#91;", "[")
            .Replace ("&#93;", "]").Replace ("&#44;", ",");
    }
}