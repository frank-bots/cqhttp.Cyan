using Newtonsoft.Json;

namespace cqhttp.Cyan.ApiCall.Requests {
    /// <summary>
    /// [在cqhttp4.8中加入]
    /// 获取语音文件转换格式后的文件名/绝对路径
    /// 请不要直接构造并发送此调用，而通过ElementRecord的成员函数调用
    /// </summary>
    [JsonObject]
    public class GetRecordRequest : Base.ApiRequest {
        [JsonProperty] string file;
        [JsonProperty] string out_format;
        [JsonProperty] bool full_path;
        /// <param name="file">收到的语音文件名（CQ 码的 file 参数）</param>
        /// <param name="out_format">要转换到的格式，目前支持 mp3、amr、wma、m4a、spx、ogg、wav、flac</param>
        /// <param name="full_path">是否返回文件的绝对路径（Windows 环境下建议使用，Docker 中不建议）</param>
        public GetRecordRequest (string file, string out_format, bool full_path = false):
            base ("/get_record") {
                this.response = new Results.GetRecordResult ();
                this.file = file;
                this.out_format = out_format;
                this.full_path = full_path;
            }
    }
}