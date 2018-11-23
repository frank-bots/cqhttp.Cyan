using System;
using cqhttp.Cyan.Elements;

namespace cqhttp.Cyan.Tests {
    public class ElementBuild {
        public static void Test () {
            try {
                ElementText testtext = new ElementText ();
                ElementImage testimage = new ElementImage ();
            } catch (NullElementException e) {
                Console.WriteLine ($"OK: NullElementException thrown::{e.Message}");
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
            } catch { }

        }
    }
}