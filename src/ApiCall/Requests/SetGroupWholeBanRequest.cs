using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 全 员 🈲️ 💬
    /// </summary>
    public class SetGroupWholeBanRequest : ApiRequest {
        long group_id;
        bool enable;
        /// <param name="group_id">要禁言的群号</param>
        /// <param name="enable">是否禁言</param>
        public SetGroupWholeBanRequest (long group_id, bool enable = true) : base ("/set_group_whole_ban") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.enable = enable;
        }
        ///
        public override string content {
            get {
                return $"{{\"group_id\"={group_id},\"enable\"={enable}}}";
            }
        }
    }
}