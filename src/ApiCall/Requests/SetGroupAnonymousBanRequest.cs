using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 群组匿名用户禁言
    /// </summary>
    public class SetGroupAnonymousBanRequest : ApiRequest {
        long group_id;
        string flag;
        long duration;
        ///
        public SetGroupAnonymousBanRequest (long group_id, string flag, long duration = 1800) : base ("/set_group_anonymous_ban") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.flag = flag;
            this.duration = duration;
        }
        ///
        public override string content {
            get {
                return $"{{\"group_id\":{group_id},\"flag\":\"{Config.asJsonStringVariable(flag)}\",\"duration\":{duration}}}";
            }
        }
    }
}