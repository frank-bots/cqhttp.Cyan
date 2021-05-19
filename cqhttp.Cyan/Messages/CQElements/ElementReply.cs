using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// at某人
    /// </summary>
    public class ElementReply : Element {
        string reply_id;
        /// <param name="reply_id">回复时引用的消息 id</param>
        public ElementReply (string reply_id) :
            base ("reply", ("id", reply_id)) {
            this.reply_id = reply_id;
        }
    }
}