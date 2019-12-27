using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetRecordResult : Base.ApiResult {
        /// <summary>
        /// 获取到的文件名或绝对路径
        /// </summary>
        public string file;
        ///
        public override void Parse (JToken result) {
            PreCheck (result);
            try {
                file = raw_data["file"].ToString ();
            } catch {
                Log.Error ("调用get_record API未返回file");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}