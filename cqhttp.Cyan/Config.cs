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
        public static int timeOut = 10;
        /// <summary>检查连接是否存活的间隔</summary>
        public const int checkAliveInterval = 10000; //10秒
    }
}