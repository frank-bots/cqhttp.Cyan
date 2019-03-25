# cqhttp.Cyan

## why "Cyan"

m gf's n c t w "Cyan"

## Parent Project

<http://cqhttp.cc>  
<https://github.com/richardchien/coolq-http-api>

## Purpose

    To make it easier to develop a bot.  

    Make C# QQ bot developers concentrate on bot logic instead of struggling with network communication with cqhttp.

## 你需要重点关注哪些部分

* [这里](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Instance.html) 是在你应当在编写bot逻辑之前应当声明的Client的prototype
* [这里](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Messages.CQElements.html) 是以Element*的命名形式命名的能够用来构造Message的消息段
* [这里](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Messages.CommonMessages.html) 是可以直接拿来当成 ```Message``` 使用的常用消息
* [这里](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Events.CQEvents.CQResponses.html) 是对上报消息的快速回复
* [这里](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Events.CQEvents.html) 是各种消息上报类型```Events```
* [这里](https://github.com/frankli0324/cqhttp.Cyan/tree/master/src/_Examples) 是你可以拿来参考的示例代码

## What You Should Care in this Doc

* [Here](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Instance.html) are Clients that you can create in the beginning of your code
* [Here](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Messages.CQElements.html) are Elements you can use to Construct Messages
* [Here](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Messages.CommonMessages.html) are Common Messages you can directly use as a ```Message```
* [Here](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Events.CQEvents.CQResponses.html) are Responses you can use to respond to events received
* [Here](https://www.std-frank.club/cqhttp.Cyan/api/cqhttp.Cyan.Events.CQEvents.html) are ```Events``` that you will eventually receive
* [Here](https://github.com/frankli0324/cqhttp.Cyan/tree/master/src/_Examples) are examples you can refer to to build a bot