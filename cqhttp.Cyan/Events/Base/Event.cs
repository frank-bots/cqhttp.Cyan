using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.Base {
    public enum PostType {
        meta_event,
        message,
        notice,
        request
    }
    /// <summary>
    /// Event指“事件上报”中的事件
    /// <see>https://cqhttp.cc/docs/4.6/#/Post</see>
    /// </summary>
    public class Event {
        public int time { get; private set; } = -1;
        public PostType postType { get; private set; }
        public Event () {
            throw new NullEventException ("调用了Event()");
        }
        public Event (int time, PostType postType) {
            this.time = time;
            this.postType = postType;
        }
    }
}