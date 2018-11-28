using System;

namespace cqhttp.Cyan {
    /// <summary>
    /// 调用了空构造函数，不能构造空对象
    /// </summary>
    public class NullElementException : Exception {
        public NullElementException () { }
        public NullElementException (string message) : base (message) { }
    }
    /// <summary>
    /// 错误地进行了消息段构造
    /// </summary>
    public class ErrorElementException : Exception {
        public ErrorElementException () : base () { }
        public ErrorElementException (string message) : base (message) { }
    }
    public class NetworkFailureException : Exception {
        public NetworkFailureException () : base () { }
        public NetworkFailureException (string message) : base (message) {}
    }
}