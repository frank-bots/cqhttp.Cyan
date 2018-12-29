namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 获取酷 Q 及 HTTP API 插件的版本信息
    /// </summary>
    public class GetVersionInfoRequest : Base.ApiRequest {
        ///
        public GetVersionInfoRequest () : base ("/get_version_info") {
            this.response = new Results.GetVersionInfoResult ();
        }
        ///
        override public string content{
            get{
                return "{}";
            }
        }
    }
}