using cqhttp.Cyan.Events.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// <see>https://cqhttp.cc/docs/4.6/#/Post?id=%E7%BE%A4%E6%96%87%E4%BB%B6%E4%B8%8A%E4%BC%A0</see>
    /// </summary>
    public class GroupFileUploadEvent : Events.Base.CQEvent {
        /// <summary>上传文件信息</summary>
        public FileInfo fileInfo { get; private set; }

        public GroupFileUploadEvent () : base () { }
        public GroupFileUploadEvent (long time) : base (time, Enums.PostType.notice) { }
    }

    [JsonObject]
    public class FileInfo {
        [JsonProperty ("id")]
        string id;

        [JsonProperty ("name")]
        string name;

        [JsonProperty ("size")]
        long size;

        [JsonProperty ("busid")]
        long busid;
    }
}