using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Messages.CommonMessages {
    /// <summary>
    /// 在线图片
    /// </summary>
    public class MessageOnlineImage : Message {
        /// <param name="url">图片的url</param>
        /// <param name="useCache">是否缓存</param>
        public MessageOnlineImage (string url, bool useCache = true):
            base (new ElementImage (url, useCache)) { }
    }
}