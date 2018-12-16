using System.Text.RegularExpressions;

namespace cqhttp.Cyan {
    /// <summary>全局设置</summary>
    public class Config {
        /// <summary>消息默认发送格式</summary>
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        public static bool isSendJson = false;
        // public const string wrapperCQCode = "[CQ:{0}{1}]";
        // public const string wrapperJSonCode = "{\"type\":\"{0}\",\"data\":{{{1}}}";

        /// <summary>网络错误时最多重试的次数</summary>
        public const int networkMaxFailure = 3;
        /// <summary>网络请求超时秒数</summary>
        public const int timeOut = 5;

        /// <summary>调用API时的验证头</summary>
        public static string apiToken = "";

        /// <summary>匹配CQ码</summary>
        public static Regex matchCqCode
            = new Regex (@"\[CQ:\w+?.*?\]");
        /// <summary>匹配CQ码的类型</summary>
        public static Regex parseCqCode
            = new Regex (@"\[CQ:(\w+)");

        /// <summary>匹配CQ码的参数</summary>
        public static Regex paramCqCode
            = new Regex (@",([\w\-\.]+?)=([^,\]]+)");

        /// <summary>
        /// 设定调用api时的验证头
        /// </summary>
        public static void SetToken (string token) => apiToken = token;
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