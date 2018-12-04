using System;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Tests {
    public class MessageBuild {
        public static void Test () {
            try {
                Message testmessage = new Message
                {
                    data = new System.Collections.Generic.List<Messages.Base.Element>
                    {
                        new ElementText("first te[xt] message"),
                        new ElementImage("http://www.asdf.com/asdf.jpg"),
                        new ElementText("second #&text message"),
                        new ElementRecord("http://asdf.com/asdf.mp3", true, false)
                    }
                };
                Console.WriteLine (Message.Serialize (testmessage));
                //[{"type":"text","data":{"text":"first text message"}},{"type":"image","data":{"file":"http://www.asdf.com/asdf.jpg"}},{"type":"text","data":{"text":"second text message"}},{"type":"record","data":{"file":"http://asdf.com/asdf.mp3"}}]

                Console.WriteLine (Message.Serialize (testmessage, false));
                //first text message[CQ:image,file=http://www.asdf.com/asdf.jpg]second text message[CQ:record,file=http://asdf.com/asdf.mp3]
            } catch { }
        }
    }
}