using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 退出讨论组
    /// </summary>
    public class SetFriendAddRequest : ApiRequest {
        string flag;
        bool approve;
        string remark;
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
        ///
        public override string content {
            get {
                return $"{{\"flag\":\"{Config.asJsonStringVariable(flag)}\",\"approve\":{approve},\"remark\":\"{Config.asJsonStringVariable(remark)}\"}}";
            }
        }
    }
}