namespace cqhttp.Cyan {
    public class Config {
        /// <summary>消息默认发送格式</summary>
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        public const bool isSendJson = true;
        public const string wrapperCQCode = "[CQ:{0},{1}]";
        //public const string wrapperJSonCode = "{\"type\":\"{0}\",\"data\":{{{1}}}";

        /// <summary>网络错误时最多重试的次数</summary>
        public const int networkMaxFailure = 3;
    }
}