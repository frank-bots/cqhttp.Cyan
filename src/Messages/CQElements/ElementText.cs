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
        public ElementText (string text):
            base ("text", ("text", text)) { this.text = text; }
    }
}