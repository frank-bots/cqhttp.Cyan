using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using cqhttp.Cyan;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan {
    class Program {
        static void Main (string[] args) {
            // Tests.ElementBuild.Test();
            //Examples.MessageBuild.Test();

            //Examples.DeserializationTest.Test ();
            
            Examples.ApiRequestTest.Test();
        }
    }
}