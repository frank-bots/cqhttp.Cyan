using System;
using System.Collections.Generic;
using cqhttp.Cyan.Messages.Base;

namespace cqhttp.Cyan.Messages {
    public class ElementFace : Element {
        public int faceId { get; private set; }
        public ElementFace () : base () { }
        /// <summary>
        /// recommended constructor for qq faces
        /// </summary>
        /// <param name="id">qq表情编号(1-170)</param>
        public ElementFace (int id):
            base ("face", ("id", id.ToString ())) { this.faceId = id; }

        public ElementFace (params (string key, string val) [] dict):
            base ("face", dict) { GetFace (); }

        public ElementFace (Dictionary<string, string> dict):
            base ("face", dict) { GetFace (); }
        private void GetFace () {
            try {
                this.faceId = int.Parse (data["id"]);
            } catch (Exception e) {
                if( e is KeyNotFoundException)
                    throw new ErrorElementException ("***data中没有id段");
                else if(e is FormatException || e is OverflowException)
                    throw new ErrorElementException ("***data中id项非数字或数字错误");
            }
        }
    }
}