using cqhttp.Cyan.Messages.CQElements;
using Newtonsoft.Json;
using Xunit;
namespace cqhttp.Cyan.Messages {
    public class EqualTest {
        [Fact]
        public void TestEqual () {
            Message m = new Message (
                new ElementText ("te st1"),
                new ElementFace ("234"),
                new ElementImage ("http://c2cpicdw.qpic.cn/offpic_new/1234//1234-1234-62E2788A257962500ECF2401DD69A76B/0?term=2")
            );
            Message n = new Message (
                new ElementText ("te st1"),
                new ElementFace ("234"),
                new ElementImage ("http://c2cpicdw.qpic.cn/offpic_new/5678//5678-5678-62E2788A257962500ECF2401DD69A76B/0?term=2")
            );
            Assert.Equal (m, n);
            n = new Message (
                new ElementText ("te st1"),
                new ElementFace ("234"),
                new ElementImage ("http://c2cpicdw.qpic.cn/offpic_new/5678//5678-5678-ECF2401DD69A76B62E2788A257962500/0?term=2")
            );
            Assert.NotEqual (m, n);
        }
    }
}