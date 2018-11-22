using System;
using System.Collections.Generic;
using cqhttp.Cyan;

namespace cqhttp.Cyan {
    class Program {
        public static string test {
            get;private set;
        }
        static void Main (string[] args) {
            test="asdf";
            Console.WriteLine (test);
            string b = new Dictionary<string, string> () ["asdf"];
            Console.WriteLine ("Hello World!");
        }
    }
}