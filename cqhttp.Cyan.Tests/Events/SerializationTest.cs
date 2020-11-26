using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
namespace cqhttp.Cyan.Events {
    public class SerializationTest {

        [Fact]
        public void TestDeserializeGroupAddRequest () {
            string raw_event = @"{
                ""comment"":""this is a comment"",
                ""flag"":""2211123"",
                ""group_id"":1234011235,
                ""post_type"":""request"",
                ""request_type"":""group"",
                ""self_id"":2333323930,
                ""sub_type"":""add"",""time"":15123412343,""user_id"":12341255}
            ";
            var test_event = JsonConvert.DeserializeObject<CQEvents.Base.CQEvent> (
                raw_event
            );
            Assert.Equal (
                typeof (GroupAddRequestEvent),
                test_event.GetType ()
            );
        }

        [Fact]
        public void TestDeserializeGroupIncreaseDecrease () {
            string raw_event = @"{
                ""post_type"":""notice"",
                ""notice_type"":""group_decrease"",
                ""group_id"":324123938,""operator_id"":73423412,""self_id"":24123450,
                ""sub_type"":""kick"",""time"":1576581502,""user_id"":295123455
            }";
            var test_event = JsonConvert.DeserializeObject<CQEvents.Base.CQEvent> (
                raw_event
            );
            Assert.Equal (
                typeof (GroupMemberChangeEvent),
                test_event.GetType ()
            );
            var gmce = test_event as GroupMemberChangeEvent;
            Assert.Equal (
                Enums.NoticeType.group_decrease,
                gmce.noticeType
            );
            Assert.Equal (
                295123455, gmce.user_id
            );
        }

        [Fact]
        public void TestDeserializeGroupMessage () {
            string raw_event = @"{
                ""message_type"":""group"",""post_type"":""message"",
                ""raw_message"":""a"",""self_id"":12345,""message_id"":22114,
                ""sender"":{
                    ""age"":0,""area"":"""",""card"":"""",
                    ""level"":""潜水"",""nickname"":""Frank™"",
                    ""role"":""owner"",""sex"":""unknown"",
                    ""title"":"""",""user_id"":712341
                },
                ""font"":3434136,""group_id"":12345,""anonymous"":null,
                ""message"":[{
                    ""data"":{""text"":""a""},
                    ""type"":""text""
                }],
                ""sub_type"":""normal"",""time"":1463725355,""user_id"":7121523
            }";
            var event_token = JToken.Parse (raw_event);
            var test_event = JsonConvert.DeserializeObject<CQEvents.Base.CQEvent> (
                raw_event
            );
            Assert.Equal (
                typeof (GroupMessageEvent),
                test_event.GetType ()
            );
            var gme = test_event as GroupMessageEvent;
            Assert.Equal (
                JToken.Parse (@"[{""type"":""text"",""data"":{""text"":""a""}}]"),
                JToken.Parse (JsonConvert.SerializeObject (gme.message))
            );
            Assert.Equal (
                (MessageType.group_, 12345),
                gme.GetEndpoint ()
            );
        }
    }
}