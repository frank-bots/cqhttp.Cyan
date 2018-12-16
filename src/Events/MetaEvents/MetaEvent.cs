using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Events.MetaEvents
{
    /// <summary>
    /// midware, dont care
    /// </summary>
    public class MetaEvent : cqhttp.Cyan.Events.CQEvents.Base.CQEvent
    {
        /// <summary></summary>
        public MetaEvent(long time, PostType postType) : base(time, postType)
        {
        }
    }
}