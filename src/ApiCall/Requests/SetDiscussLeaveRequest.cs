using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 退出讨论组
    /// </summary>
    public class SetDiscussLeaveRequest : ApiRequest {
        long discuss_id;
        /// <param name="discuss_id">讨论组 ID（正常情况下看不到，需要从讨论组消息上报的数据中获得）</param>
        public SetDiscussLeaveRequest (long discuss_id) : base ("/set_discuss_leave") {
            this.response = new Results.EmptyResult ();
            this.discuss_id = discuss_id;
        }
        ///
        public override string content {
            get {
                return $"{{\"discuss_id\":{discuss_id}}}";
            }
        }
    }
}