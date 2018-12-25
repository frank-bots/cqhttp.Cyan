namespace cqhttp.Cyan.ApiCall.Requests {
    ///
    public class GetGroupMemberInfoRequest : Base.ApiRequest {
        long group_id, user_id;
        bool no_cache;

        ///
        public GetGroupMemberInfoRequest (long group_id, long user_id, bool no_cache = false):
            base ("/get_group_member_info") {
                this.response = new Result.GetGroupMemberInfoResult ();
                this.group_id = group_id;
                this.user_id = user_id;
                this.no_cache = no_cache;
            }
        ///
        override public string content {
            get {
                return $"{{\"group_id\":{group_id},\"user_id\":{user_id},\"no_cache\":{no_cache}}}";
            }
        }
    }
}