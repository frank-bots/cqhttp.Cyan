using System;
using System.Collections.Generic;
using cqhttp.Cyan.Messages.CQElements.Base;
namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary></summary>
    public class ElementEmoji : Element {
        int id;
        /// <summary></summary>
        public ElementEmoji (int id):
            base ("emoji", ("id", id.ToString ())) { this.id = id; }
    }
}