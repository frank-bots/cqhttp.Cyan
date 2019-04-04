using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 退出讨论组
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class SetFriendAddRequest : ApiRequest {
        [Newtonsoft.Json.JsonProperty] string flag;
        [Newtonsoft.Json.JsonProperty] bool approve;
        [Newtonsoft.Json.JsonProperty] string remark;
        /// <param name="flag">加好友请求的 flag（需从上报的数据中获得）</param>
        /// <param name="approve">是否同意请求</param>
        /// <param name="remark">添加后的好友备注（仅在同意时有效）</param>
        /// <returns></returns>
        public SetFriendAddRequest (string flag, bool approve = true, string remark = "") : base ("/set_friend_add") {
            this.response = new Results.EmptyResult ();
            this.flag = flag;
            this.approve = approve;
            this.remark = remark;
        }
    }
}