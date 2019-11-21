using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Messages.CommonMessages {
    /// <summary>
    /// 分享链接
    /// </summary>
    public class MessageShare : Message {
        /// <param name="url">分享链接</param>
        /// <param name="title">分享标题</param>
        /// <param name="content">简介，可为空</param>
        /// <param name="image">图片链接，可为空</param>
        public MessageShare (string url, string title, string content = "", string image = ""):
            base (new ElementShare (url, title, content, image)) { }
    }
}