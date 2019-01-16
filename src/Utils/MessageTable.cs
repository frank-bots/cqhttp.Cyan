using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Messages;

namespace src.Utils {
    /// <summary>
    /// 记录发送的消息
    /// </summary>
    public class MessageTable {
        List < (long, Message) > messageList = new List < (long, Message) > ();
        /// <summary>对消息进行记录以便后续撤回</summary>
        public void Log (long mid, Message event_) {
            messageList.Add ((mid, event_));
            Task.Run (() => {
                Thread.Sleep (120000);
                try { messageList.Remove ((mid, event_)); } catch { }
            });
        }
        /// <summary></summary>
        public long GetMessageId (string pattern) {
            Regex pat = new Regex (pattern);
            foreach (var i in messageList) {
                if (pat.IsMatch (i.Item2.raw_data_cq)) {
                    return i.Item1;
                }
            }
            throw new cqhttp.Cyan.Exceptions.NullEventException ("未发现满足条件的消息");
        }
    }
}