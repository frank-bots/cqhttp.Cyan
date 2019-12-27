using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 记录发送的消息
    /// </summary>
    public class MessageTable : List < (long, Message) > {
        /// <summary>对消息进行记录以便后续撤回</summary>
        internal void Log (long mid, Message event_) {
            Add ((mid, event_));
            Task.Run (() => {
                Thread.Sleep (120000);
                Remove ((mid, event_));
            });
        }
        private new bool Remove ((long, Message) obj) {
            return base.Remove (obj);
        }
        /// <summary></summary>
        public long GetMessageId (string pattern) {
            Regex pat = new Regex (pattern);
            foreach (var i in this) {
                if (pat.IsMatch (i.Item2.ToString ()))
                    return i.Item1;
            }
            throw new cqhttp.Cyan.Exceptions.NullEventException ("未发现满足条件的消息");
        }
    }
}