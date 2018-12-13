namespace cqhttp.Cyan.Events.CQEvents.CQResponses.Base {
    /// <summary></summary>
    public abstract class CQReplyMessageResponse : CQResponse {
        /// <summary></summary>
        public Messages.Message reply { get; private set; }
        /// <summary></summary>
        public bool auto_escape { get; private set; }
        /// <summary></summary>
        public CQReplyMessageResponse (Messages.Message reply, bool auto_escape) {
            this.reply = reply;
            this.auto_escape = auto_escape;
        }
    }
}