namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary></summary>
    public class GetLoginInfoRequest : Base.ApiRequest {
        /// <summary></summary>
        public GetLoginInfoRequest () : base ("/get_login_info") { }
        /// <summary></summary>
        override public string content { get { return "{}"; } }
    }
}