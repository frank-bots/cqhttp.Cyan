using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetImageResult : Base.ApiResult {
        /// <summary>
        /// 获取到的绝对路径
        /// </summary>
        public string file;
        ///
        public override void Parse (JToken result) {
            this.PreCheck (result);
            try {
                file = raw_data["file"].ToString ();
            } catch {
                Log.Error ("调用get_image API未返回file");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}