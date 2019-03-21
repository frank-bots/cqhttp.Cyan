using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 设置群组能否匿名聊天
    /// </summary>
    public class SetGroupAnonymousRequest : ApiRequest {
        long group_id;
        bool enable;
        ///
        public SetGroupAnonymousRequest (long group_id, bool enable = true) : base ("/set_group_anonymous") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.enable = enable;
        }
        ///
        public override string content {
            get {
                return $"{{\"group_id\":{group_id},\"enable\":{enable}}}";
            }
        }
    }
}