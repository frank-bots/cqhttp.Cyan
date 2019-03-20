using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 给好友的名片点个赞👍
    /// </summary>
    public class SendLikeRequest : ApiRequest {
        long user_id;
        int times;
        ///
        public SendLikeRequest (long user_id, int times) : base ("/send_like") {
            this.response = new Results.EmptyResult ();
            this.user_id = user_id;
            this.times = times;
        }
        ///
        public override string content {
            get {
                return $"{{\"user_id\"={user_id},\"times\"={times}}}";
            }
        }
    }
}