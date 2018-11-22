using System.Collections.Generic;

namespace cqhttp.Cyan.Elements.Base {
    /// <summary>
    /// 消息元素，即cqhttp所定义的消息段
    /// </summary>
    /// <remarks>
    /// should NEVER be constructed or used directly
    /// </remarks>
    public class Element {
        public string type { get; private set; }
        /// <summary>
        /// represents the raw message 
        /// </summary>
        public virtual Dictionary<string, string> data { get; private set; }
        public string raw_data {
            get {
                string ret = "{";
                foreach (var i in data)
                    ret += $"{i.Key}:{Encoder.Encode(i.Value)},";
                ret += '}';
                return ret;
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
    
}