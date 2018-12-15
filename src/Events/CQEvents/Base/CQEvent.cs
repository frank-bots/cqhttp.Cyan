using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Events.CQEvents.Base
{

    /// <summary>
    /// Event指“事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    public abstract class CQEvent {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; private set; } = -1;
        /// <summary>
        /// 事件类型
        /// </summary>
        public PostType postType { get; private set; }
        /// <summary>
        /// 请勿默认构造，本项目中一切默认构造函数仅为测试所用
        /// </summary>
        public CQEvent () {
            throw new Exceptions.NullEventException ("调用了Event()");
        }
        /// <summary>
        /// 底层构造事件
        /// </summary>
        public CQEvent (long time, PostType postType) {
            this.time = time;
            this.postType = postType;
        }
    }
}