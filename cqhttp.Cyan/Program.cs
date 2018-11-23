using System;
using System.Collections.Generic;
using cqhttp.Cyan;

namespace cqhttp.Cyan {
    class Program {
        public static string test {
            get;private set;
        }
        static void Main (string[] args) {
            // Console.WriteLine (Convert.ToChar('a'+1));
            // string b = new Dictionary<string, string> () ["asdf"];
            // Console.WriteLine ("Hello World!");
            Tests.ElementBuild.Test();
            Tests.MessageBuild.Test();
        }
    }
}