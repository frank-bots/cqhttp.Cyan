using System.Collections.Generic;

namespace cqhttp.Cyan.Messages.Base {
    /// <summary>
    /// 消息元素，即cqhttp所定义的消息段
    /// </summary>
    /// <remarks>
    /// should NEVER be constructed or used directly
    /// </remarks>
    public class Element {
        public string type { get; private set; }
        /// <summary>
        /// represents the true message 
        /// </summary>
        public virtual Dictionary<string, string> data { get; private set; }

        /// <summary>
        /// builds the value when constructing CQCode
        /// <see>https://d.cqp.me/Pro/CQ%E7%A0%81</see>
        /// <see cref=ElementText.raw_data_cq/>
        /// </summary>
        /// <value>CQCode</value>
        public string raw_data_cq {
            get {
                if (type == "text")
                    return Encoder.EncodeText (data["text"]);
                string paramBuilder = "";
                foreach (var i in data)
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
                string builder = $"{{\"type\":\"{type}\",\"data\":"+(data.Count!=0?$"{{":"\"");
                foreach (var i in data)
                    builder += $"\"{i.Key}\":\"{i.Value.Replace("\"","\\\"")}\",";
                return builder.TrimEnd (',', ' ')+(data.Count!=0?$"}}": "\"")+$"}}";
                //。。。不这么写的话我的代码格式化插件就会崩掉
            }
        }
        public Element () {
            throw new NullElementException ("调用了Element()");
        }
        public Element (string type, params (string key, string value) [] dict) {
            this.type = type;
            data = new Dictionary<string, string> ();
            foreach (var i in dict)
                data.Add (i.key, i.value);
        }
        public Element (string type, Dictionary<string, string> dict) {
            this.type = type;
            data = dict;
        }
    }
    public class Encoder {
        public static string EncodeText (string enc) => enc.Replace ("&", "&amp").Replace ("[", "&#91").Replace ("]", "&#93");
        public static string EncodeValue (string text) => EncodeText (text).Replace (",", "&#44");
        public static string Decode (string enc) => enc.Replace ("&amp", "&").Replace ("&#91", "[")
            .Replace ("&#93", "]").Replace ("&#44", ",");
    }
}