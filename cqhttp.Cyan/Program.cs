using System;
using System.Collections.Generic;
using cqhttp.Cyan;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan {
    class Program {
        /*
{
    "font":31108208,
    "message":"asdf",
    "message_id":1002,
    "message_type":"private",
    "post_type":"message",
    "raw_message":"asdf",
    "self_id":2956005355,
    "sender":{
        "age":48,
        "nickname":"Frank\xe2\x84\xa2",
        "sex":"male",
        "user_id":745679136
    },
    "sub_type":"friend",
    "time":1543816446,
    "user_id":745679136
}
        */
        class A{}
        class B:A{}
        class C:B{}
        static void Main (string[] args) {
            //JArray jArray=JArray.Parse("{\"type\":\"text\",\"data\":{\"text\":\"asdf\"}}");
            //Console.WriteLine(jArray[0]);
            // Console.WriteLine (Convert.ToChar('a'+1));
            // string b = new Dictionary<string, string> () ["asdf"];
            // Console.WriteLine ("Hello World!");
             Tests.ElementBuild.Test();
             Tests.MessageBuild.Test();
        }
    }
}