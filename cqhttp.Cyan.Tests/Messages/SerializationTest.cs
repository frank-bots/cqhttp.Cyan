using cqhttp.Cyan.Messages.CQElements;
using Newtonsoft.Json;
using Xunit;
namespace cqhttp.Cyan.Messages {
    public class SerializationTest {
        [Fact]
        public void TestSerialize () {
            Message m = new Message (
                new ElementText ("te st1"),
                new ElementFace ("234")
            );
            string mjson = "[{\"type\":\"text\",\"data\":{\"text\":\"te st1\"}},{\"type\":\"face\",\"data\":{\"id\":\"234\"}}]";
            string mcq = "te st1[CQ:face,id=234]";
            Assert.Equal (
                mjson, JsonConvert.SerializeObject (m)
            );

            Assert.Equal (mjson, JsonConvert.SerializeObject (m));
            Assert.Equal (mcq, m.ToString ());
        }
    }
}