
using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// 获取图片路径
    /// </summary>
    public class GetImageRequest : ApiRequest {
        string filename;
        ///
        public GetImageRequest (string filename) : base ("/get_image") {
            this.response = new Results.GetImageResult();
            this.filename = filename;
        }
        ///
        public override string content {
            get {
                return $"{{\"filename\"={filename}}}";
            }
        }
    }
}
