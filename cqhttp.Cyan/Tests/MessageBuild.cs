using System;
using cqhttp.Cyan.Elements;

namespace cqhttp.Cyan.Tests {
    public class MessageBuild {
        public static void Test () {
            try {
                Message testmessage = new Message ();
                testmessage.data =
                    new System.Collections.Generic.List<Elements.Base.Element> {
                        new ElementText ("first text message"),
                        new ElementImage ("http://www.asdf.com/asdf.jpg"),
                        new ElementText ("second text message"),
                        new ElementRecord ("http://asdf.com/asdf.mp3", true, false)
                    };
                Console.WriteLine (MessageSerializer.Serialize (testmessage));
                //[{"type":"text","data":{"text":"first text message"}},{"type":"image","data":{"file":"http://www.asdf.com/asdf.jpg"}},{"type":"text","data":{"text":"second text message"}},{"type":"record","data":{"file":"http://asdf.com/asdf.mp3"}}]

                Console.WriteLine (MessageSerializer.Serialize (testmessage, false));
                //first text message[CQ:image,file=http://www.asdf.com/asdf.jpg]second text message[CQ:record,file=http://asdf.com/asdf.mp3]
            } catch { }
        }
    }
}