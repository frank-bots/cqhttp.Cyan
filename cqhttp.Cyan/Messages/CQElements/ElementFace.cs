using System;
using System.Collections.Generic;
using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>表情</summary>
    public class ElementFace : Element {
        string faceId;
        /// <summary>
        /// recommended constructor for qq faces
        /// </summary>
        /// <param name="id">qq表情编号(1-170)</param>
        /// <param name="type">
        /// 可以是:
        ///     QQ表情(face)
        ///     emoji表情(emoji)
        ///     原创表情(bface)
        ///     小表情(sface)
        /// 其中之一
        /// 具体请参考<a href="https://d.cqp.me/Pro/CQ%E7%A0%81">酷Q码文档</a>中相关部分
        /// </param>
        public ElementFace (string id, string type = "face"):
            base (type, ("id", id)) { this.faceId = id; }
    }
}