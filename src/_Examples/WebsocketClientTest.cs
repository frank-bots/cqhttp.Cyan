using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Instance;
using System;
using System.Collections.Generic;
using System.Text;

namespace cqhttp.Cyan.Examples
{
    class WebsocketClientTest
    {
        public static void Test()
        {
            //设置日志
            Logger.LogType = Enums.LogType.Console | Enums.LogType.File;
            Logger.LogLevel = Enums.Verbosity.ALL;

            string accessUrl = "ws://127.0.0.1:6700";
            string token = "";
            string eventUrl = "ws://127.0.0.1:6700/event/";
            var cli = new CQWebsocketClient(accessUrl, token, eventUrl);

            var users = new List<long>() { 1234567890 };
            cli.OnEventAsync += async (client, e) =>
            {
                // 复读指定用户私聊消息
                if (e is PrivateMessageEvent me && users.Contains(me.sender_id))
                {
                    await client.SendMessageAsync(
                        Enums.MessageType.private_,
                        me.sender_id,
                        me.message
                    );
                }
                return new EmptyResponse();
            };
            System.Console.ReadLine();// 务必在所有bot中加上这一句
        }
    }
}
