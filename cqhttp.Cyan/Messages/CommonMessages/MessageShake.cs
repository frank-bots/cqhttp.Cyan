namespace cqhttp.Cyan.Messages.CommonMessages {
    /// <summary>
    /// 戳一戳
    /// </summary>
    public class MessageShake : Message {
        /// <summary>
        /// 构造一个窗口抖动,貌似仅在使用CQ码模式下发送有效
        /// </summary>
        public MessageShake ():
            base (new Messages.CQElements.ElementShake ()) { }
    }
}