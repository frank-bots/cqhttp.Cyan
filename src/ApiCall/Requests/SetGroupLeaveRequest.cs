using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 退出/解散群组
    /// </summary>
    public class SetGroupLeaveRequest : ApiRequest {
        long group_id;
        bool is_dismiss;
        /// <param name="group_id">群号</param>
        /// <param name="is_dismiss">是否解散，如果登录号是群主，则仅在此项为 true 时能够解散</param>
        public SetGroupLeaveRequest (long group_id, bool is_dismiss = false) : base ("/set_group_leave") {
            this.response = new Results.EmptyResult ();
            this.group_id = group_id;
            this.is_dismiss = is_dismiss;
        }
        ///
        public override string content {
            get {
                return $"{{\"group_id\":{group_id},\"is_dismiss\":{is_dismiss}}}";
            }
        }
    }
}