namespace cqhttp.Cyan.Events.CQEvents.CQResponses {
    /// <summary>是否同意好友请求响应</summary>
    public class FriendAddResponse : Base.CQResponse {
        bool approve;
        string remark;
        /// <param name="approve">是否加为好友</param>
        /// <param name="remark">好友备注</param>
        public FriendAddResponse (bool approve, string remark) {
            this.approve = approve;
            this.remark = remark;
        }
        /// <summary></summary>
        public override string content {
            get {
                return $"{{\"approve\":{approve},\"remark\":\"{remark}\"}}";
            }
        }
    }
}