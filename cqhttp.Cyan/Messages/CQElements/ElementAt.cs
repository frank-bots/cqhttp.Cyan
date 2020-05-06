using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// at某人
    /// </summary>
    public class ElementAt : Element {
        long user_id;
        /// <param name="user_id">at的用户QQ号</param>
        public ElementAt (long user_id) :
            base ("at", ("qq", user_id.ToString ())) {
            this.user_id = user_id;
        }
        /// <summary>表示at全体成员</summary>
        public ElementAt () : base ("at", ("qq", "all")) { }
    }
}