# cqhttp.Cyan

    Cyan---青

## 快速上手

```csharp
using cqhttp.Cyan.[];
namespace YourNS {
    class Program {
        static void Main(string[] args) {
            //CQApiClient client = CQWebsocketClient(
            CQApiClient client = CQHTTPClient (
                //accessUrl: "ws://domain:port/path",
                accessUrl: "http://domain:port/path",
                accessToken: "your_token",
                listen_port: 8080,
                //Websocket 监听仍在调试中，请暂时不要使用
                //然鹅可以通过Websocket发送API请求
                secret: "your_secret"
            )
            Console.WriteLine(
                $"QQ:{client.self_id},昵称:{client.self_nick}"
            );
            //client构造后会发送一条get_login_info请求，则可以通过
            //判断是否成功获取登陆的账号的QQ与昵称判断API是否可访问
            client.OnEventDelegate += (client, e) => {
                //process (CQEvent e)
                return new CQEmptyResponse ();
            };
            client.SendTextAsync(
                MessageType.private_,
                12345678,
                "一条文字消息"
            );
        }
    }
}
```

## 对应关系

|cqhttp中的概念|对应本项目中的类型|所处在的命名空间|
|--------------:|:-----------:|:-----------:|
|消息|```Message```|.Messages|
|消息段|```Element```|.Messages.CQElements(.Base)|
|事件|```CQEvent```|.Events.CQEvents(.Base)|
|事件响应|```CQResponse```|.Events.CQResponse(.Base)|
|Api调用|```ApiRequest```|.ApiCall.Requests(.Base)|
|Api响应|```ApiResponse```|.ApiCall.Requests(.Base)|

## 消息的发送与接收

本项目中发送与接收到的消息同为Message类型。  

### 消息接收

在事件处理函数中可通过```(e is MessageEvent)```判断收到的是否为消息事件，如下：

```csharp
static void Main() {
    var client = HTTPApiClient(...);
    client.OnEventDelegate += HandleEvent;
}
static void HandleEvent(CQApiClient api,CQEvent e) {
    if(e is MessageEvent) {
        HandleMessage(
            (e as MessageEvent).message
        )
    }
}
```

### 消息发送

可以自己构建需要发送的消息，也可以选择直接发送文本消息，亦或是发送定义好的常用消息  

目前可用的常用消息:

1. MessageShake(), 戳一戳
2. ~~MessageText(), 纯文本~~, 由于提供了发送纯文本消息的函数，故不需要

```csharp
static void Main() {
    var client = HTTPApiClient(...);
    //////////////////////
    client.SendTextAsync("text");
    //////////////////////
    client.SendMessageAsync(
        MessageType.private_/group_/discuss_,
        qq号/群号/讨论组号,
        new Message {
            data=new List<Element> {
                new ElementText("part1"),
                new ElementFace(1),//1-170
                new ElementText("part2")
            }
        }
    )
    //////////////////////
}
```

目前可用的Element类型:

* ```ElementEmoji(int id)```
* ```ElementFace(int id)```
* ```ElementImage(string url,bool useCache = false)```
* ........................```(byte[] resourse, bool useCache = false)```
* ```ElementRecord(string url,bool useCache = false)```
* ..........................```(byte[] resourse, bool useCache = false)```
* ```ElementShake()```

> 出于不知道什么原因ElementShake()只有在以非json发送消息时才有效

## 事件列表

### 消息事件(继承于MessageEvent)

* PrivateMessageEvent
* GroupMessageEvent
* DiscussMessageEvent

### 群通知(继承于GroupNoticeEvent)

* GroupAdminEvent 群管理员变动
* GroupUploadEvent 群文件上传
* GroupMemberChangeEvent 群成员变动

#### 另有FriendAddEvent与GroupNoticeEvent同继承于NoticeEvent

### 请求事件(继承于RequestEvent)

* GroupAddRequestEvent 邀请加群
* FriendAddRequestEvent 添加好友
> 需要作出回应(....不回应也罢)