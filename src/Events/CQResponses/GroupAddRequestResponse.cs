namespace cqhttp.Cyan.Events.CQResponses {
    /// <summary>是否同意加群</summary>
    [Newtonsoft.Json.JsonObject]
    public class GroupAddRequestResponse : Base.CQResponse {
        [Newtonsoft.Json.JsonProperty] bool approve;
        [Newtonsoft.Json.JsonProperty] string reason;
        /// <param name="approve">是否同意</param>
        /// <param name="remark">如果不同意的话理由是啥？</param>
        public GroupAddRequestResponse (bool approve, string remark = "") {
            this.approve = approve;
            this.reason = remark;
        }
    }
}