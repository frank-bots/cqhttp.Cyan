using System;
using System.Collections.Generic;
using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary></summary>
    public class ElementFace : Element {
        int faceId;
        /// <summary>
        /// recommended constructor for qq faces
        /// </summary>
        /// <param name="id">qq表情编号(1-170)</param>
        public ElementFace (int id):
            base ("face", ("id", id.ToString ())) { this.faceId = id; }
    }
}