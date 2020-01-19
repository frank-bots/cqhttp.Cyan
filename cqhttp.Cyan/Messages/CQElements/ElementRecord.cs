using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// 语音消息
    /// </summary>
    public class ElementRecord : ElementFile {

        /// <summary>是否变声</summary>
        /// <see>https://d.cqp.me/Pro/CQ%E7%A0%81</see>
        public bool isMagic { get; private set; }
        /// <summary></summary>
        public ElementRecord (byte[] record, bool useCache = true, bool isMagic = false):
            base ("record", record, useCache) {
                this.isMagic = isMagic;
                if (isMagic) data["magic"] = "true";
            }
        /// <summary></summary>
        public ElementRecord (string url, bool useCache = true, bool isMagic = false):
            base ("record", url, useCache) {
                this.isMagic = isMagic;
                if (isMagic) data["magic"] = "true";
            }

    }
}