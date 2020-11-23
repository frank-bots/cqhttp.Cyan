using System.Collections.Generic;
using cqhttp.Cyan.Messages.CQElements;
using cqhttp.Cyan.Messages.CQElements.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Messages {
    /// <summary>
    /// 由消息段构成的消息
    /// </summary>
    [JsonConverter (typeof (MessageConverter))]
    public class Message {
        /// <summary>
        /// 消息段列表
        /// </summary>
        public List<Element> data;
        /// <param name="elements">构建的消息段列表</param>
        public Message (params Element[] elements) {
            this.data = new List<Element> (elements);
        }
        /// <summary>纯文本的默认构造</summary>
        public Message (string text) : this (new ElementText (text)) { }
        /// <summary>
        /// 拼接消息
        /// </summary>
        public static Message operator + (Message a, Message b) {
            a.data.AddRange (b.data);
            return a;
        }
        /// <summary>
        /// 向消息拼接消息段
        /// </summary>
        public static Message operator + (Message a, Element b) {
            a.data.Add (b);
            return a;
        }
        ///
        public static bool operator == (Message a, Message b) => a.Equals (b);
        ///
        public static bool operator != (Message a, Message b) => !a.Equals (b);

        /// <summary>
        /// 序列化消息为CQ码
        /// </summary>
        public override string ToString () {
            return string.Join ("", data);
        }
        /// <summary></summary>
        public override int GetHashCode () {
            return this.data.GetHashCode ();
        }
        /// <summary></summary>
        public override bool Equals (object a) {
             if (a is Message that) {
                if (this.data.Count != that.data.Count)
                    return false;
                for (int i = 0; i < this.data.Count; i++) {
                    if (this.data[i] != that.data[i])
                        return false;
                }
                return true;
            } else return false;
        }
        /// <summary>
        /// 下载所有文件元素(图片,语音)
        /// </summary>
        public async void FixAsync () {
            foreach (var i in data)
                if (i is ElementFile file)
                    await file.Fix ();
        }
    }
}