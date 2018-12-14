using System;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;
namespace cqhttp.Cyan.Examples {
    /// <summary>
    /// 
    /// </summary>
    public class MessageBuild {
        /// <summary>
        /// 
        /// </summary>
        public static void Test () {
            try {
                Message testmessage = new Message (
                    new ElementText ("first te[xt] message"),
                    new ElementImage ("http://www.asdf.com/asdf.jpg"),
                    new ElementText ("second #&text message"),
                    new ElementRecord ("http://asdf.com/asdf.mp3", true, false)
                );
                Console.WriteLine (testmessage.raw_data_json);
                //[{"type":"text","data":{"text":"first text message"}},{"type":"image","data":{"file":"http://www.asdf.com/asdf.jpg"}},{"type":"text","data":{"text":"second text message"}},{"type":"record","data":{"file":"http://asdf.com/asdf.mp3"}}]

                Console.WriteLine (testmessage.raw_data_cq);
                //first text message[CQ:image,file=http://www.asdf.com/asdf.jpg]second text message[CQ:record,file=http://asdf.com/asdf.mp3]
            } catch { }
        }
    }
}