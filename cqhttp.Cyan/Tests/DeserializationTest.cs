using System;
using cqhttp.Cyan.Events;
using cqhttp.Cyan.Events.Base;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Tests {
    public class DeserializationTest {
        public static void Test () {
            short res;
            Message test = Message.Deserialize ("asdf", out res);

            test = Message.Deserialize ("[CQ:image,url=asdf.net]asdf[CQ:image,url=asdf.net]", out res);



            string messageEvent = "{\"font\":31108208,\"message\":\"asdf\",\"message_id\":1002,\"message_type\":\"private\",\"post_type\":\"message\",\"raw_message\":\"asdf\",\"self_id\":2956005355,\"sender\":{\"age\":48,\"nickname\":\"Frank\xe2\x84\xa2\",\"sex\":\"male\",\"user_id\":745679136},\"sub_type\":\"friend\",\"time\":1543816446,\"user_id\":745679136}";
            CQEvent a = CQEventHandler.Handle(messageEvent);
            Console.WriteLine(Message.Serialize((a as PrivateMessageEvent).message));
        }
    }
}