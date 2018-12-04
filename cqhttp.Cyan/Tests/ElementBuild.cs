using System;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.Tests {
    public class ElementBuild {
        public static void Test () {
            try {
                //ElementText testtext = new ElementText ();
                //ElementImage testimage = new ElementImage ();
                //ElementFace testface = new ElementFace();
                ElementEmoji testemoji = new ElementEmoji ();
            } catch (NullElementException e) {
                Console.WriteLine ($"OK: NullElementException thrown::{e.Message}");
            }
            try {
                ElementEmoji errorEmoji = new ElementEmoji (("asdf", "asdf"));
                ElementFace errorFace = new ElementFace (("asdf", "asdf"));
            } catch (ErrorElementException e) {
                Console.WriteLine ($"OK: ErrorElementException thrown::{e.Message}");
            }
            try {
                ElementImage testimage = new ElementImage ("http://www.asdf.com/asdf.jpg");
                ElementText testtext = new ElementText ("some text");
                ElementRecord testvoice = new ElementRecord ("http://asdf.com/a.mp3", true, true);
                // Xunit.Assert(testimage.raw_data_cq)
                // Console.WriteLine (testimage.raw_data_cq);
                // Console.WriteLine (testimage.raw_data_json + '\n' + '\n');
                // Console.WriteLine (testtext.raw_data_cq);
                // Console.WriteLine (testtext.raw_data_json + '\n' + '\n');
                Console.WriteLine (testvoice.raw_data_cq);
                Console.WriteLine (testvoice.raw_data_json + '\n' + '\n');
            } catch {
                // ignored
            }

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