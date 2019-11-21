using cqhttp.Cyan;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Instance;

namespace cqhttp.Cyan.Examples {
    /// <summary>
    /// 复读机示例
    /// </summary>
    public class Repeater {
        ///
        public static void Main_ () {
            var cli = new CQHTTPClient (
                accessUrl: "http://egurl.com"
            );
            cli.OnEventAsync += async (client, e) => {
                // 只复读群消息
                if (e is GroupMessageEvent) {
                    var me = (e as GroupMessageEvent);
                    await client.SendMessageAsync (
                        Enums.MessageType.group_,
                        me.group_id,
                        me.message
                    );
                }
                return new EmptyResponse ();
            };
            System.Console.ReadLine();// 务必在所有bot中加上这一句
        }
    }
}