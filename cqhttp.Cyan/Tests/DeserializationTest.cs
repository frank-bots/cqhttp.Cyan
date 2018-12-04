using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Tests
{
    public class DeserializationTest
    {
        public static void Test(){
            short res;
            Message test = Message.Deserialize("asdf",out res);
            
            test = Message.Deserialize("[CQ:image,url=asdf.net]asdf[CQ:image,url=asdf.net]",out res);
        }
    }
}