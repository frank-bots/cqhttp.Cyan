using System.Text.RegularExpressions;

namespace cqhttp.Cyan {
    /// <summary>全局设置</summary>
    public class Config {
        /// <summary>消息默认发送格式</summary>
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        public static bool isSendJson = false;

        /// <summary>网络错误时最多重试的次数</summary>
        public const int networkMaxFailure = 3;
        /// <summary>网络请求超时秒数</summary>
        public const int timeOut = 5;

        /// <summary>匹配CQ码</summary>
        public static readonly Regex matchCqCode
            = new Regex (@"\[CQ:\w+?.*?\]");
        /// <summary>匹配CQ码的类型</summary>
        public static readonly Regex parseCqCode
            = new Regex (@"\[CQ:(\w+)");

        /// <summary>匹配CQ码的参数</summary>
        public static readonly Regex paramCqCode
            = new Regex (@",([\w\-\.]+?)=([^,\]]+)");

        /// <summary>将字符串转义为json值</summary>
        public static string asJsonStringVariable (string str) =>
            str
                .Replace("\\","\\\\")
                .Replace ("\"", "\\\"")
                .Replace ("\n", "\\n")
                .Replace("\r","\\r")
                ;
    }
}