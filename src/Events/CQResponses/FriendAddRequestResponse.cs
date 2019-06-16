namespace cqhttp.Cyan.Events.CQResponses {
    /// <summary>是否同意好友请求响应</summary>
    [Newtonsoft.Json.JsonObject]
    public class FriendAddResponse : Base.CQResponse {
        [Newtonsoft.Json.JsonProperty] bool approve;
        [Newtonsoft.Json.JsonProperty] string remark;
        /// <param name="approve">是否加为好友</param>
        /// <param name="remark">好友备注</param>
        public FriendAddResponse (bool approve, string remark) {
            this.approve = approve;
            this.remark = remark;
        }
    }
}