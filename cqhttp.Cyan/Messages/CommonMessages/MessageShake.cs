using cqhttp.Cyan.Messages.Base;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Messages.CommonMessages {
    public class MessageShake : Message {
        public MessageShake () : base () {
            data = new System.Collections.Generic.List<Element> {
                new ElementShake ()
            };
        }
    }
}