namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>分享链接</summary>
    /// <remark>
    /// 本消息段只能单独发送,
    /// 请通过<see cref="CommonMessages.MessageShare"/>发送
    /// </remark>
    public class ElementShare : Base.BaseElementShare {
        ///
        public ElementShare (string url, string title, string content = "", string image = ""):
            base (url, title, content, image) { }
    }
}