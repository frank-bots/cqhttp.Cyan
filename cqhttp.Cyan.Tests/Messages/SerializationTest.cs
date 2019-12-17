using cqhttp.Cyan.Messages.CQElements;
using Newtonsoft.Json;
using Xunit;
namespace cqhttp.Cyan.Messages {
    public class SerializationTest {
        [Fact]
        public void TestSerialize () {
            Message m = new Message (
                new ElementText ("test1"),
                new ElementFace ("234")
            );
            string mjson = "[{\"type\":\"text\",\"data\":{\"text\":\"test1\"}},{\"type\":\"face\",\"data\":{\"id\":\"234\"}}]";
            string mcq = "test1[CQ:face,id=234]";
            Assert.Equal (
                mjson, JsonConvert.SerializeObject (m)
            );

            cqhttp.Cyan.Config.isSendJson = true;
            Assert.Equal (mjson, m.ToString ());
            cqhttp.Cyan.Config.isSendJson = false;
            Assert.Equal (mcq, m.ToString ());
        }
    }
}