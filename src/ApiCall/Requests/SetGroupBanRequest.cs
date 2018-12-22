using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 堵上一个用户的嘴
    /// </summary>
    public class SetGroupBanRequest : SetGroupMemberStatusRequest {
        long duration;
        /// <param name="group_id">群号</param>
        /// /// <param name="user_id">用户QQ</param>
        /// <param name="duration">禁言时长</param>
        public SetGroupBanRequest (long group_id, long user_id, long duration):
            base ("/set_group_ban", group_id, user_id) {
                this.response=new Result.EmptyResult();
                this.duration = duration;
            }
        /// <summary></summary>
        override public string content {
            get {
                return 
                    $"{{\"group_id\":{group_id},"+
                    $"\"user_id\":{user_id},"+
                    $"\"duration\":{duration}}}";
            }
        }
    }
}