namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 获取加入的群列表
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class GetGroupListRequest : ApiCall.Requests.Base.ApiRequest {
        ///
        public GetGroupListRequest () : base ("/get_group_list") {
            this.response = new Results.GetGroupListResult ();
        }
    }
}