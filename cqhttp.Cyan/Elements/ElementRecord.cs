using cqhttp.Cyan.Elements.Base;

namespace cqhttp.Cyan.Elements {
    class ElementRecord : ElementFile {

        /// <summary>是否变声</summary>
        /// <see>https://d.cqp.me/Pro/CQ%E7%A0%81</see>
        public bool isMagic { get; private set; }
        public ElementRecord () : base () { }
        public ElementRecord (byte[] record, bool useCache = true, bool isMagic = false):
            base ("record", record, useCache) {
                this.isMagic = isMagic;
                if(isMagic)data["magic"] = "true";
            }
        public ElementRecord (string url, bool useCache = true, bool isMagic = false):
            base ("record", url, useCache) {
                this.isMagic = isMagic;
                if(isMagic)data["magic"] = "true";
            }

    }
}