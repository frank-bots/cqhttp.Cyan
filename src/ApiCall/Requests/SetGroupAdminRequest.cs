using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 群组设置管理员
    /// </summary>
    public class SetGroupAdminRequest : ApiRequest {
        long group_id;
        long user_id;
        bool enable;
        ///
        public SetGroupAdminRequest (long group_id, long user_id, bool enable) : base ("/set_group_admin") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.user_id = user_id;
            this.enable = enable;
        }
        ///
        public override string content {
            get {
                return $"{{\"group_id\":{group_id},\"user_id\":{user_id},\"enable\":{enable}}}";
            }
        }
    }
}