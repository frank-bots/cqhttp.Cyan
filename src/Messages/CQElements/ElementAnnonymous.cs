namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>表示匿名发消息</summary>
    public class ElementAnnonymous : Base.Element {
        /// <summary></summary>
        /// <param name="ignore">
        /// 表示是否匿名发消息
        /// </param>
        /// <remark>
        /// 本消息段必须处于消息最前,
        /// 请使用<see cref="CommonMessages.MessageAnnonymous"/>构造
        /// </remark>
        /// <returns></returns>
        public ElementAnnonymous (bool ignore):
            base ("annonymous", ("ignore", (ignore? "true": "false"))) {

            }
    }
}