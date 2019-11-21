using cqhttp.Cyan.Events.CQEvents.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// <see>https://cqhttp.cc/docs/4.6/#/Post?id=%E7%BE%A4%E6%96%87%E4%BB%B6%E4%B8%8A%E4%BC%A0</see>
    /// </summary>
    public class GroupUploadEvent : GroupNoticeEvent {
        /// <summary>上传文件信息</summary>
        public FileInfo fileInfo { get; private set; }
        /// <summary></summary>
        public GroupUploadEvent (long time, FileInfo fileInfo, long group_id, long user_id):
            base (time, Enums.NoticeType.group_upload, group_id, user_id) {
                this.fileInfo = fileInfo;
            }
    }
    /// <summary></summary>
    [JsonObject]
    public class FileInfo {
        /// <summary></summary>
        public string id { get; private set; }
        /// <summary>文件名</summary>

        public string name { get; private set; }
        /// <summary></summary>

        public long size { get; private set; }
        /// <summary>cqhttp作者也不知道，我也不知道是干啥的(总线id？那是啥)</summary>

        public long busid { get; private set; }
    }
}