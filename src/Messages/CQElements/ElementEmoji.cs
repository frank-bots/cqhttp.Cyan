using System;
using System.Collections.Generic;
using cqhttp.Cyan.Messages.CQElements.Base;
namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary></summary>
    public class ElementEmoji : Element {
        int id;
        /// <summary></summary>
        public ElementEmoji () : base () { }
        /// <summary></summary>
        public ElementEmoji (params (string, string) [] data):
            base ("emoji", data) { GetEmoji(); }
        /// <summary></summary>
        public ElementEmoji (Dictionary<string, string> data):
            base ("emoji", data) { GetEmoji(); }
        /// <summary></summary>
        public ElementEmoji (int id):
            base ("emoji", ("id", id.ToString ())) { this.id = id; }

        private void GetEmoji () {
            try {
                this.id = int.Parse (data["id"]);
            } catch (Exception e) {
                if (e is KeyNotFoundException)
                    throw new ErrorElementException ("***data中没有id段");
                else if (e is FormatException || e is OverflowException)
                    throw new ErrorElementException ("***data中id项非数字或数字错误");
            }
        }
    }
}