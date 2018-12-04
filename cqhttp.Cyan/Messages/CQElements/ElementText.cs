using System.Collections.Generic;
using cqhttp.Cyan.Messages.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    public class ElementText : Element {
        public string text { get; private set; }
        public ElementText() : base() { }
        public ElementText (string text):
            base ("text", ("text", text)) { GetText(); }

        public ElementText (params (string key, string val) [] dict):
            base ("text", dict) { GetText (); }

        public ElementText (Dictionary<string, string> dict):
            base ("text", dict) { GetText (); }
        private void GetText () {
            try {
                this.text = data["text"];
            } catch (KeyNotFoundException) {
                throw new ErrorElementException ("data中没有text段***");
            }
        }
    }
}