using System;
using cqhttp.Cyan.ApiHTTP.Requests;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Tests {
    public class ApiRequestTest {
        public static void Test () {
            /*
In [34]: p={"message_type":"private","user_id":745679136,"message":[{"type":"tex
    ...: t","data":{"text":"first te[xt] message"}},{"type":"image","data":{"fil
    ...: e":"https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=231051439
    ...: 0,3580363630&fm=27&gp=0.jpg"}},{"type":"text","data":{"text":"second #&
    ...: text message"}}]}

In [35]: x=requests.post('http://service.std-frank.club:233/send_msg',json=p)
ok
             */
            Message testmessage = new Message {
                data = new System.Collections.Generic.List<Messages.Base.Element> {
                new ElementText ("first te[xt] message"),
                new ElementFace (3),
                new ElementText ("second #&text message")
                }
            };
            ApiClient client = new ApiClient ("http://service.std-frank.club:233");
            // Console.WriteLine (
            //     client.SendMessageAsync (MessageType.private_, 745679136, testmessage).Result.data.ToString()
            // );
            // Console.WriteLine (
            //     client.SendTextAsync (MessageType.private_, 745679136, "testmessage").Result.data.ToString()
            // );
            // Console.WriteLine (
            //     client.SendMessageAsync (MessageType.private_, 745679136, new Messages.CommonMessages.MessageShake()).Result.data.ToString()
            // );
        }
    }
}