using System;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Examples {
    /// <summary>
    /// 
    /// </summary>
    public class ElementBuild {
        /// <summary>
        /// 
        /// </summary>
        public static void Test () {
            try {
                
                ElementImage testFix = new ElementImage ("https://i.loli.net/2018/11/28/5bfe41395dcbd.jpg");
                testFix.Fix ().Wait ();
                Console.WriteLine (testFix.raw_data_json);
                
            } catch (Exception e) { Console.WriteLine (e.Message); }
            ElementShake testShake = new ElementShake();
            Console.WriteLine(testShake.raw_data_cq + "\n" + testShake.raw_data_json);
        }
    }
}