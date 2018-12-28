using System;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQResponses;
using cqhttp.Cyan.Instance;
using cqhttp.Cyan.Messages;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Examples
{
    /// <summary></summary>
    public class ApiRequestTest {
        /// <summary></summary>
        public static void Test () {
            Message testmessage = new Message (
                new ElementText ("first te[xt] message"),
                new ElementFace (3),
                new ElementText ("second #&text message")
            );
            var clientHttp = new CQHTTPClient (
                accessUrl: "http://domain.com:port",//请不要在后面加多余的/斜线，没有进行判定
                accessToken: "token",               //api token
                listen_port : 256,                  //本地监听端口
                secret: "secret"                    //消息上报secret
            );
            clientHttp.OnEventDelegate += (cli, e) => {
                Console.WriteLine ((e as GroupMessageEvent).message.raw_data_json);
                return new EmptyResponse ();
            };
            var clientWebsocket = new CQWebsocketClient(
                accessUrl: "ws://domain.com:port",  //请不要在后面加多余的/斜线，没有进行判定
                accessToken: "token"               //api token
            );//暂时只能发送api请求
            Console.ReadLine ();
        }
    }
}