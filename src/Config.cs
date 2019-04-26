using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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
        /// <summary>检查连接是否存活的间隔</summary>
        public const int checkAliveInterval = 10000; //10秒

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
            .Replace ("\\", "\\\\")
            .Replace ("\"", "\\\"")
            .Replace ("\n", "\\n")
            .Replace ("\r", "\\r");

        /// <summary>
        /// 若condition在<see cref="timeOut"/>秒后仍然为否, 抛出异常
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="e">超时后抛出的异常</param>
        /// <param name="interval">检查条件的间隔(毫秒)</param>
        public static async Task TimeOut (
            System.Func<bool> condition,
            string e,
            int interval = 200
        ) {
            int cnt = 0;
            while (condition () == false && cnt++ * interval < timeOut * 1000)
                await Task.Run (() => Thread.Sleep (interval));
            if (condition () == false) {
                Logger.Log (Enums.Verbosity.ERROR, $"操作超时: " + e);
                throw new Exceptions.NetworkFailureException (e);
            }
        }
    }
}