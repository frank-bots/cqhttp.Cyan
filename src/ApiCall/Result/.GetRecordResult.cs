using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetRecordResult : Base.ApiResult {
        /// <summary>
        /// 获取到的文件名或绝对路径
        /// </summary>
        public string file;
        ///
        public override void Parse (string result) {
            var i = this.PreCheck (result);
            try {
                file = i.raw_data["file"].ToString ();
            } catch {
                Logger.Error ("调用get_record API未返回file");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}