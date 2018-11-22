using System.Collections.Generic;
using cqhttp.Cyan.Elements.Base;

namespace cqhttp.Cyan {
    public class Message {
        public List<Element> data;
    }
    public class MessageSerializer {
        /// <summary>
        /// 将消息序列化便于发送或本地存储
        /// </summary>
        /// <remarks>
        /// 依据参数<c/>isSendJson</c>判断格式化的结果
        /// </remarks>
        /// <param name="message">待序列化的消息</param>
        /// <param name="isSendJson">是否序列化为json格式，默认为true</param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialize (Message message, bool isSendJson = Config.isSendJson) {
            if (isSendJson) return SerializeToJson (message);
            else return SerializeToCQ (message);
        }
        /// <summary>
        /// 将收到或构造的消息反序列化以存储在内存中
        /// </summary>
        /// <param name="message">字符串形式的消息</param>
        /// <returns>反序列化后的<c>Message</c>对象</returns>
        public static Message Deserialize (string message) {
            return new Message ();
            // TODO:
        }

        /// <summary>
        /// 将消息序列化为Json格式，即
        /// https://cqhttp.cc/docs/4.6/#/Message 中的
        /// 数组格式
        /// </summary>
        /// <return>
        /// 数组格式的消息
        /// </return>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string SerializeToJson (Message message) {
            string jsonBuild = "{";
            foreach (var i in message.data)
                jsonBuild += i.raw_data;
            jsonBuild += "}";
            return jsonBuild;
        }
        /// <summary>
        /// 将消息序列化为CQ码格式，即酷Q原生消息格式
        /// 亦即<c>SerializeToJson</c>中页面所说的
        /// 字符串格式
        /// </summary>
        /// <see cref=SerializeToJson>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string SerializeToCQ (Message message) {
            string cqBuild = "";
            foreach (Element i in message.data)
                cqBuild += i.raw_data;
            return "";
        }
    }
    public class Encoder {
        public static string Encode (string enc) 
            => enc.Replace ("&", "&amp").Replace ("[", "&#91").Replace ("]", "&#93");
        public static string EncodeText (string text)
            => Encode(text).Replace(",", "&#44");
        public static string Decode (string enc)
            => enc.Replace ("&amp", "&").Replace ("&#91", "[").Replace ("&#93", "]");
    }
}