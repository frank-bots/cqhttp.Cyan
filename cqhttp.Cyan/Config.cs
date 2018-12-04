using System.Text.RegularExpressions;

namespace cqhttp.Cyan {
    public class Config {
        /// <summary>消息默认发送格式</summary>
        /// <see>https://cqhttp.cc/docs/4.6/#/Message</see>
        public const bool isSendJson = true;
        public const string wrapperCQCode = "[CQ:{0}{1}]";
        //public const string wrapperJSonCode = "{\"type\":\"{0}\",\"data\":{{{1}}}";

        /// <summary>网络错误时最多重试的次数</summary>
        public const int networkMaxFailure = 3;

        /// <summary>
        /// 匹配CQ码
        /// </summary>
        public static Regex matchCqCode
            = new Regex (@"\[CQ:\w+?.*?\]");
        /// <summary>
        /// 匹配CQ码的类型
        /// </summary>
        /// <returns></returns>
        public static Regex parseCqCode
            = new Regex (@"\[CQ:(\w+)");
        
        /// <summary>
        /// 匹配CQ码的参数
        /// </summary>
        public static Regex paramCqCode
            = new Regex (@",([\w\-\.]+?)=([^,\]]+)");
    }
}