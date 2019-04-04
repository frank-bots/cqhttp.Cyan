namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 获取群员列表,列表项为<see cref="Utils.GroupMemberInfo"/>
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class GetGroupMemberListRequest : Base.ApiRequest {
        [Newtonsoft.Json.JsonProperty] long group_id;
        /// <param name="group_id">群号码</param>
        /// <param name="no_cache">是否使用缓存</param>
        /// <returns></returns>
        public GetGroupMemberListRequest (long group_id, bool no_cache = false):
            base ("/get_group_member_list") {
                if (no_cache)
                    throw new Exceptions.ErrorApicallException ("暂不支持此API调用不启用缓存，请逐次调用GetGroupMemberInfoRequest");
                this.response = new Results.GetGroupMemberListResult ();
                this.group_id = group_id;
            }
    }
}