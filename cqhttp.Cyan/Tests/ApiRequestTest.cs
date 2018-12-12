using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Instance;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Tests {
    /// <summary></summary>
    public class ApiRequestTest {
        /// <summary></summary>
        public static void Test () {
            Message testmessage = new Message {
                data = new System.Collections.Generic.List<Messages.CQElements.Base.Element> {
                new ElementText ("first te[xt] message"),
                new ElementFace (3),
                new ElementText ("second #&text message")
                }
            };
            // CQHTTPClient client = new CQHTTPClient ("http://service.std-frank.club:233");
            var client = new CQHTTPClient (
                accessUrl: "http://service.std-frank.club:233",
                listen_port : 256
            );
            client.OnEventDelegate += (cli, e) => {
                Console.WriteLine ((e as GroupMessageEvent).message.raw_data_json);
                return new CQEmptyResponse ();
            };
            //var i = client.SendTextAsync (MessageType.private_, 745679136, "test").Result;
            //var j = client.SendMessageAsync (MessageType.private_, 745679136, testmessage).Result;
            Console.ReadLine ();

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