using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 用于清空插件的日志文件。
    /// </summary>
    public class CleanDataDirRequest : ApiRequest {
        string data_dir;
        ///
        public CleanDataDirRequest (string data_dir) : base ("/clean_data_dir") {
            this.response = new Results.EmptyResult ();
            this.data_dir = data_dir;
        }
        ///
        public override string content {
            get {
                return $"{{\"data_dir\":\"{Config.asJsonStringVariable(data_dir)}\"}}";
            }
        }
    }
}