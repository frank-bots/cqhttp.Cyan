using Newtonsoft.Json.Linq;
using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Events.Base {

    /// <summary>
    /// Event指“事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    public class CQEvent {
        public long time { get; private set; } = -1;
        public PostType postType { get; private set; }
        public CQEvent () {
            throw new NullEventException ("调用了Event()");
        }
        public CQEvent (long time, PostType postType) {
            this.time = time;
            this.postType = postType;
        }
    }
}