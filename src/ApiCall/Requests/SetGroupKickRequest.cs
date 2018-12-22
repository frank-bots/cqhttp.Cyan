using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 群组踢人
    /// </summary>
    public class SetGroupKickRequest : SetGroupMemberStatusRequest {
        bool reject_add_request;
        /// <param name="group_id">群号码</param>
        /// <param name="user_id">要踢的用户QQ号</param>
        /// <param name="reject_add_request">是否拒绝加群请求</param>
        public SetGroupKickRequest (long group_id, long user_id, bool reject_add_request = false) : base ("/set_group_kick", group_id, user_id) {
            this.response = new Result.EmptyResult ();
            this.group_id = group_id;
            this.user_id = user_id;
            this.reject_add_request = reject_add_request;
        }
        /// <summary></summary>
        override public string content {
            get {
                return $"{{\"group_id\":{group_id},\"user_id\":{user_id},\"reject_add_request\":{reject_add_request}}}";;
            }
        }
    }
}