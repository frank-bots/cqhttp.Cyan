using System;
using cqhttp.Cyan.Events;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Examples {
    /// <summary>
    /// 反序列化模块的使用
    /// </summary>
    public class DeserializationTest {
        /// <summary>
        /// 
        /// </summary>
        public static void Test () {
            Message test = Message.Parse ("asdf");

            test = Message.Parse ("[CQ:image,url=asdf.net]asdf[CQ:image,url=asdf.net]");

            // 昵称为user，QQ号为987654321的用户向QQ号为123456789的bot发送了"asdf"文字消息
            string messageEvent = "{\"font\":31108208,\"message\":\"asdf\",\"message_id\":1000,\"message_type\":\"private\",\"post_type\":\"message\",\"raw_message\":\"asdf\",\"self_id\":123456789,\"sender\":{\"age\":1,\"nickname\":\"user\",\"sex\":\"male\",\"user_id\":987654321},\"sub_type\":\"friend\",\"time\":1500000000,\"user_id\":987654321}";
            CQEvent a = CQEventHandler.HandleEvent (messageEvent);
            Console.WriteLine ((a as PrivateMessageEvent).message.raw_data_json);
        }
    }
}