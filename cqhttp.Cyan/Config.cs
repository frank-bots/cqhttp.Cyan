namespace cqhttp.Cyan {
    /// <summary>全局设置</summary>
    public class Config {
        /// <summary>消息默认发送格式</summary>
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        public static bool is_send_json = false;

        /// <summary>网络错误时最多重试的次数</summary>
        public const int network_max_failure = 3;
        /// <summary>网络请求超时秒数</summary>
        public static int timeout = 10;
        /// <summary>默认日志等级</summary>
        public static Enums.Verbosity log_default_verbosity = Enums.Verbosity.WARN;
        /// <summary>默认日志输出位置</summary>
        public static Enums.LogType log_default_type = Enums.LogType.console;
    }
}