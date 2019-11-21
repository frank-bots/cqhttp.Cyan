using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {

    ///
    [JsonObject]
    public class GetGroupMemberInfoRequest : Base.ApiRequest {
        [JsonProperty] long group_id;
        [JsonProperty] long user_id;
        [JsonProperty] bool no_cache;

        ///
        public GetGroupMemberInfoRequest (long group_id, long user_id, bool no_cache = false):
            base ("/get_group_member_info") {
                this.response = new Results.GetGroupMemberInfoResult ();
                this.group_id = group_id;
                this.user_id = user_id;
                this.no_cache = no_cache;
            }
    }
}