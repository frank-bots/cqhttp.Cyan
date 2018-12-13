namespace cqhttp.Cyan.Events.CQEvents.CQResponses.Base {
    /// <summary></summary>
    public abstract class CQResponse {
        /// <summary></summary>
        public virtual string content {
            get {
                throw new System.NotImplementedException();
            }
        }
        /// <summary></summary>
        public override string ToString(){
            return this.content;
        }
    }
}