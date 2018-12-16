namespace cqhttp.Cyan.ApiCall.Requests.Base {
    /// <summary></summary>
    public class SetGroupMemberStatusRequest : ApiRequest {
        /// <summary></summary>
        protected long group_id, user_id;
        /// <summary></summary>
        public SetGroupMemberStatusRequest (string apiPath, long group_id, long user_id):
            base (apiPath) {
                this.group_id = group_id;
                this.user_id = user_id;
            }
    }
}