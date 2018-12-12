namespace cqhttp.Cyan.Events.CQResponses.Base {
    /// <summary></summary>
    public abstract class CQResponse {
        /// <summary></summary>
        public virtual string content {
            get {
                return "";
            }
        }
        /// <summary></summary>
        public override string ToString(){
            return this.content;
        }
    }
}