# cqhttp.Cyan

Cyan---青

## 目标

写一个看完[cqhttp的文档](https://cqhttp.cc/docs)就能上手的cqhttp C# SDK


## 文档

[Hello World](https://frank-bots.github.io/cqhttp.Cyan/guide/getting_started.html)

## 对应关系

|cqhttp中的概念|对应本项目中的类型|所处在的命名空间|
|--------------:|:-----------:|:-----------:|
|消息|```Message```|.Messages|
|消息段|```Element```|.Messages.CQElements(.Base)|
|事件|```CQEvent```|.Events.CQEvents(.Base)|
|事件响应|```CQResponse```|.Events.CQResponse(.Base)|
|Api调用|```ApiRequest```|.ApiCall.Requests(.Base)|
|Api响应|```ApiResponse```|.ApiCall.Responses(.Base)|

------------------------------

# TODO:
添加一些必要的测试  
为websocket添加消息队列  