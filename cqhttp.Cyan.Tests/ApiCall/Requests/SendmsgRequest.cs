using cqhttp.Cyan.Messages.CQElements;
using Xunit;

namespace cqhttp.Cyan.ApiCall.Requests {
    public class SendmsgRequestTest {
        [Fact]
        public static void TestSerializeRequest () {
            SendmsgRequest request = new SendmsgRequest (
                Enums.MessageType.group_, 123, new Messages.Message (
                    new ElementText ("te st1"),
                    new ElementFace ("234")
                )
            );
            Config.is_send_json = false;
            Assert.Equal (
                @"{""message_type"":""group"",""group_id"":123,""message"":""te st1[CQ:face,id=234]""}",
                request.content
            );
            Config.is_send_json = true;
            Assert.Equal (
                @"{""message_type"":""group"",""group_id"":123,""message"":[{""type"":""text"",""data"":{""text"":""te st1""}},{""type"":""face"",""data"":{""id"":""234""}}]}",
                request.content
            );
        }
    }
}