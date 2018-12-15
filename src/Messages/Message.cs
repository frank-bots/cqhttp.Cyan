using System.Collections.Generic;
using System.Text.RegularExpressions;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages.CQElements;
using cqhttp.Cyan.Messages.CQElements.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Messages {
    /// <summary>
    /// 由消息段构成的消息
    /// </summary>
    public class Message {
        //any cq code
        /// <summary>
        /// 消息段列表
        /// </summary>
        public List<Element> data;
        /// <param name="elements">构建的消息段列表</param>
        public Message (params Element[] elements) {
            this.data = new List<Element> (elements);
        }
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
        /// <summary></summary>
        public static bool operator == (Message a, Message b) {
            return a.data.Equals (b.data);
        }
        /// <summary></summary>
        public static bool operator != (Message a, Message b) {
            return !(a == b);
        }
        /// <summary>
        /// 将消息序列化为CQ码格式
        /// </summary>
        public string raw_data_cq {
            get {
                Logger.Log (Verbosity.DEBUG, "显式将消息转化为CQ码格式");
                return Serialize (this, false);
            }
        }
        /// <summary>
        /// 将消息序列化为Json格式
        /// </summary>
        public string raw_data_json {
            get {
                Logger.Log (Verbosity.DEBUG, "显式将消息转化为json数组格式");
                return Serialize (this, true);
            }
        }
        /// <summary>
        /// 按照默认设置序列化消息
        /// </summary>
        public override string ToString () {
            return Serialize (this, Config.isSendJson);
        }
        /// <summary></summary>
        public override int GetHashCode () {
            return this.data.GetHashCode ();
        }
        /// <summary></summary>
        public override bool Equals (object a) {
            return this.data == (a as Message).data;
        }
        private static Dictionary<string, string> tempDict = new Dictionary<string, string> ();

        /// <param name="message">待序列化的消息</param>
        /// <param name="isSendJson">是否序列化为json格式，默认为true</param>
        /// <returns>序列化后的字符串</returns>
        private static string Serialize (Message message, bool isSendJson = true) {
            if (isSendJson) return SerializeToJsonArray (message);
            else return SerializeToCQ (message);
        }
        /*
        asdf[CQ:image,file=DE69C8D4C54997FC5ECBE475153651BE.jpg,url=https://c2cpicdw.qpic.cn/offpic_new/745679136//73da0548-ac93-4d5e-abd8-71138f019b28/0?vuin=2956005355&amp;term=2]
         */
        /// <param name="message">字符串形式的消息</param>
        /// <param name="result">返回反序列化的结果,原消息为Json则为1,含CQ码则为-1,纯文本则为0</param>
        /// <returns>反序列化后的<c>Message</c>对象</returns>
        public static Message Deserialize (string message, out short result) {
            Message ret = new Message { data = new List<Element> () };

            Logger.Log (
                Verbosity.DEBUG,
                $"反序列化消息{(message.Length>5?message.Substring(0,5)+"...":message)}"
            );
            try {
                JArray ifParse = JArray.Parse (message);
                Logger.Log (Verbosity.DEBUG, "收到的消息为json格式");
                tempDict.Clear ();
                foreach (var i in ifParse) {
                    tempDict = i["data"].ToObject<Dictionary<string, string>> ();
                    ret.data.Add (
                        BuildElement (i["type"].ToString (), tempDict)
                    );
                }
                result = 1;
                return ret;
            } catch (JsonException) {
                Match match = Config.matchCqCode.Match (message);
                Logger.Log (Verbosity.DEBUG, "收到的消息为字符串格式");
                if (match.Success) {
                    while (match.Success) {
                        if (match.Index > 0)
                            ret.data.Add (
                                new ElementText (
                                    message.Substring (0, match.Index)
                                )
                            );
                        //try { //for production
                        ret.data.Add (BuildCQElement (match.Value));
                        //} catch { }
                        message = message.Substring (match.Index + match.Length);
                        match = Config.matchCqCode.Match (message);
                    }
                    result = -1;
                    return ret;
                }
            }
            result = 0;
            return new Message { data = new List<Element> () { new ElementText (message) } };
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
        /// <returns>序列化后的json字符串</returns>
        private static string SerializeToJsonArray (Message message) {
            string jsonBuild = "[";
            foreach (var i in message.data)
                jsonBuild += i.raw_data_json + ',';
            return jsonBuild.TrimEnd (' ', ',') + ']';
        }
        /// <summary>
        /// 将消息序列化为CQ码格式，即酷Q原生消息格式
        /// 亦即<c>SerializeToJson</c>中页面所说的
        /// 字符串格式
        /// </summary>
        /// <see cref="Message.SerializeToJsonArray(Message)"/>
        /// <param name="message"></param>
        /// <returns>序列化后的CQ码</returns>
        private static string SerializeToCQ (Message message) {
            string cqBuild = "\"";
            foreach (Element i in message.data)
                cqBuild += i.raw_data_cq;
            return cqBuild + "\"";
        }

        private static Element BuildCQElement (string cqcode) {
            Logger.Log (Verbosity.DEBUG, $"正在为CQ码{cqcode}构筑消息段");
            string type = Config.parseCqCode.Match (cqcode).Groups[1].Value;
            tempDict.Clear ();
            foreach (Match i in Config.paramCqCode.Matches (cqcode))
                tempDict.Add (i.Groups[1].Value, i.Groups[2].Value);
            return BuildElement (type, tempDict);
        }
        private static Element BuildElement (string type, Dictionary<string, string> dict) {
            try {
                switch (type) {
                    case "text":
                        return new ElementText (dict["text"]);
                    case "image":
                        return new ElementImage (dict["url"]);
                    case "record":
                        return new ElementRecord (dict["file"]);
                    case "face":
                    case "emoji":
                        return new ElementEmoji (int.Parse (dict["id"]));
                    case "shake":
                        return new ElementShake ();
                }
            } catch (KeyNotFoundException) {
                throw new Exceptions.ErrorElementException ($"type为{type}的元素反序列化过程中缺少必要的参数");
            }

            throw new Exceptions.NullElementException ($"未能解析type为{type}的元素");
        }
    }

}