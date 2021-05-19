using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Events.CQEvents.Base {
    /// <summary></summary>
    [Newtonsoft.Json.JsonConverter (
        typeof (DiscriminatedJsonConverter),
        typeof (NoticeEventDiscriminatorOptions)
    )]
    public abstract class NoticeEvent : CQEvent {
        /// <summary></summary>
        public string notice_type { get; set; }
        /// <summary></summary>
        public NoticeType noticeType {
            get {
                foreach (var x in System.Enum.GetValues (typeof (NoticeType)))
                    if (x.ToString () == notice_type)
                        return (NoticeType) x;
                throw new Exceptions.ErrorEventException ("invalid notice_type");
            }
        }
        /// <summary></summary>
        public long user_id { get; set; }
    }
}