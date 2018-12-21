using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 一些常用的工具
    /// </summary>
    public class Convert {
        //善用代码折叠
        static string[] emojiDict = {
            "😲",
            "😳",
            "😍",
            "😧",
            "😏",
            "😭",
            "😇",
            "🤐",
            "💤",
            "🥺",
            "😰",
            "😡",
            "😜",
            "😬",
            "🙂",
            "🙁",
            "😦",
            "😤",
            "🤮",
            "捂嘴笑",
            "😊",
            "🤔",
            "🤨",
            "😐",
            "😴"
        };
        /// <summary>
        /// 将QQ表情转换为emoji
        /// </summary>
        /// <param name="faceId">表情编号(1-170)</param>
        public static string ToEmoji (int faceId) {
            if (faceId < emojiDict.Length)
                return emojiDict[faceId];
            else return "未知表情";
        }
        /// <summary></summary>
        public static string ToEmoji (ElementFace i) {
            return ToEmoji (int.Parse (i.data["id"]));
        }
    }
    /// <summary>
    /// 记录发送的消息
    /// </summary>
    public class MessageTable {
        Queue<MessageEvent> queue = new Queue<MessageEvent> ();
        Task monitor;
        CancellationTokenSource monitorCanceller = new CancellationTokenSource ();
        /// <summary></summary>
        ~MessageTable () {
            monitorCanceller.Cancel ();
        }
        /// <summary></summary>
        public MessageTable () {
            monitor = Task.Run (() => {
                while (true) {
                    Thread.Sleep (1000);
                    if (queue.Count > 0) {
                        if (queue.Peek ().time - DateTime.Now.ToFileTime () > 2 * 1)
                            queue.Dequeue ();

                    }
                }
            }, monitorCanceller.Token);
        }
        /// <summary>对消息进行记录以便后续撤回</summary>
        public void Log (MessageEvent event_) =>
            queue.Enqueue (event_);
        /// <summary></summary>
        public void GetMessageId (string pattern) {
            //不知道怎么写好
        }
    }
}