using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 获取图片路径
    /// </summary>
    [Newtonsoft.Json.JsonObject]
    public class GetImageRequest : ApiRequest {
        [Newtonsoft.Json.JsonProperty] string filename;
        ///
        public GetImageRequest (string filename) : base ("/get_image") {
            this.response = new Results.GetImageResult ();
            this.filename = filename;
        }
    }
}