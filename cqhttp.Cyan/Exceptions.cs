using System;

namespace cqhttp.Cyan.Exceptions {
    /// <summary>
    /// 调用了空构造函数，不能构造空对象
    /// </summary>
    public class NullElementException : Exception {
        /// <summary></summary>
        public NullElementException () { }
        /// <summary></summary>
        public NullElementException (string message) : base (message) { }
    }
    /// <summary>
    /// 错误地进行了消息段构造
    /// </summary>
    public class ErrorElementException : Exception {
        /// <summary></summary>
        public ErrorElementException () : base () { }
        /// <summary></summary>
        public ErrorElementException (string message) : base (message) { }
    }
    /// <summary>
    /// 消息构造格式错误
    /// </summary>
    public class ErrorMessageException : Exception {
        /// <summary></summary>
        public ErrorMessageException () : base () { }
        /// <summary></summary>
        public ErrorMessageException (string message) : base (message) { }
    }
    /// <summary></summary>
    public class NetworkFailureException : Exception {
        /// <summary></summary>
        public NetworkFailureException () : base () { }
        /// <summary></summary>
        public NetworkFailureException (string message) : base (message) { }
    }
    /// <summary></summary>
    public class NullEventException : Exception {
        /// <summary></summary>
        public NullEventException () : base () { }
        /// <summary></summary>
        public NullEventException (string message) : base (message) { }
    }
    /// <summary></summary>
    public class ErrorEventException : Exception {
        /// <summary></summary>
        public ErrorEventException () : base () { }
        /// <summary></summary>
        public ErrorEventException (string message) : base (message) { }
    }
    /// <summary></summary>
    public class NullApicallException : Exception {
        /// <summary></summary>
        public NullApicallException () : base () { }
        /// <summary></summary>
        public NullApicallException (string message) : base (message) { }
    }
    /// <summary></summary>
    public class ErrorApicallException : Exception {
        /// <summary></summary>
        public ErrorApicallException () : base () { }
        /// <summary></summary>
        public ErrorApicallException (string message) : base (message) { }
    }
    /// <summary>正常状况，表示限速调用</summary>
    public class AsyncApicallException : Exception {
        /// <summary></summary>
        public AsyncApicallException () : base () { }
        /// <summary></summary>
        public AsyncApicallException (string message) : base (message) { }
    }
    /// <summary></summary>
    public class ErrorResponseException : Exception {
        /// <summary></summary>
        public ErrorResponseException () : base () { }
        /// <summary></summary>
        public ErrorResponseException (string message) : base (message) { }
    }
    ///
    public class ErrorUtilOperationException : Exception {
        ///
        public ErrorUtilOperationException () : base () { }
        ///
        public ErrorUtilOperationException (string message) : base (message) { }
    }
}