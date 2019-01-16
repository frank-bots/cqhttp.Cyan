namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// 联系人分享
    /// </summary>
    public class ElementContact : Base.Element {
        /// <summary>
        /// 构造联系人分享
        /// </summary>
        /// <param name="type">联系人类型</param>
        /// <param name="id">联系人id</param>
        public ElementContact (Enums.MessageType type, long id):
            base (
                "contact",
                ("id", id.ToString ()),
                ("type", type == Enums.MessageType.private_? "private":
                    type == Enums.MessageType.group_? "group":
                    "discuss"
                )) { }
    }
}