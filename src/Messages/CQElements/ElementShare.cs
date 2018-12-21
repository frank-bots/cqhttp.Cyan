using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>分享链接</summary>
    /// <remark>
    /// 本消息段只能单独发送,
    /// 请通过<see cref="CommonMessages.MessageShare"/>发送
    /// </remark>
    public class ElementShare : Element {
        ///
        public ElementShare (string url, string name, string content = "", string image = "") : base ("share", ("url", url), ("name", name), ("content", content), ("image", image)) {
            this.isSingle = true;
        }
    }
}