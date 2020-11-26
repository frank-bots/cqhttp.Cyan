namespace cqhttp.Cyan {
    /// <summary>全局设置</summary>
    public class Config {
        /// <summary>消息默认发送格式</summary>
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        public static bool is_send_json = true;

        /// <summary>网络错误时最多重试的次数</summary>
        public const int network_max_failure = 3;
        /// <summary>网络请求超时秒数</summary>
        public static int timeout = 10;
        /// <summary>默认日志等级</summary>
        public static Enums.Verbosity internal_logger_verbosity {
            get {
                return Utils.Logger.GetLogger ("cqhttp.Cyan").log_level;
            }
            set {
                Utils.Logger.GetLogger ("cqhttp.Cyan").log_level = value;
            }
        }
        /// <summary>默认日志输出位置</summary>
        public static Enums.LogType internal_logger_output {
            get {
                return Utils.Logger.GetLogger ("cqhttp.Cyan").log_type;
            }
            set {
                Utils.Logger.GetLogger ("cqhttp.Cyan").log_type = value;
            }
        }
    }
}