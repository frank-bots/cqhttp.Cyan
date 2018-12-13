namespace cqhttp.Cyan.Events.CQEvents.CQResponses {
    /// <summary>是否同意加群</summary>
    public class GroupAddRequestResponse : Base.CQResponse {
        bool approve;
        string reason;
        /// <param name="approve">是否同意</param>
        /// <param name="remark">如果不同意的话理由是啥？</param>
        public GroupAddRequestResponse (bool approve, string remark = "") {
            this.approve = approve;
            this.reason = remark;
        }
        /// <summary></summary>
        public override string content {
            get {
                return $"{{\"approve\":{approve},\"reason\":\"{reason}\"}}";
            }
        }
    }
}