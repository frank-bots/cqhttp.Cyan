using System.Collections.Generic;
using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// 文本消息段
    /// </summary>
    public class ElementText : Element {
        ///
        public string text { get; private set; }
        /// <summary></summary>
        public ElementText() : base() { }
        /// <summary></summary>
        public ElementText (string text):
            base ("text", ("text", text)) { GetText(); }
        /// <summary></summary>
        public ElementText (params (string key, string val) [] dict):
            base ("text", dict) { GetText (); }
        /// <summary></summary>
        public ElementText (Dictionary<string, string> dict):
            base ("text", dict) { GetText (); }
        private void GetText () {
            try {
                this.text = data["text"];
            } catch (KeyNotFoundException) {
                throw new ErrorElementException ("data中没有text段***");
            }
        }
    }
}