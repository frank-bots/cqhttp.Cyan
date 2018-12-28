namespace cqhttp.Cyan.Events.CQResponses {
    /// <summary>不知所措？那就返回空值吧！www</summary>
    public class EmptyResponse : Base.CQResponse {
        /// <summary></summary>
        public override string content {
            get{
                return "";
            }
        }
    }
}