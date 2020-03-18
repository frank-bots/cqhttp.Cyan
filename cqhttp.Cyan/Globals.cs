using System.Text.RegularExpressions;

namespace cqhttp.Cyan {
    internal static class Log {
        static Utils.Logger logger = Utils.Logger.GetLogger ("cqhttp.Cyan");
        static Log () {
            logger.log_type =
                System.Diagnostics.Process.GetCurrentProcess ().MainWindowHandle !=
                System.IntPtr.Zero ? Enums.LogType.file : Enums.LogType.console;
        }
        public static void Info (string message) =>
            logger.Info (message);
        public static void Debug (string message) =>
            logger.Debug (message);
        public static void Warn (string message) =>
            logger.Warn (message);
        public static void Error (string message) =>
            logger.Error (message);
        public static void Fatal (string message) =>
            logger.Fatal (message);
    }
    internal static class Patterns {
        /// <summary>匹配CQ码</summary>
        public static readonly Regex matchCqCode
            = new Regex (@"\[CQ:\w+?.*?\]");
        /// <summary>匹配CQ码的类型</summary>
        public static readonly Regex parseCqCode
            = new Regex (@"\[CQ:(\w+)");

        /// <summary>匹配CQ码的参数</summary>
        public static readonly Regex paramCqCode
            = new Regex (@",([\w\-\.]+?)=([^,\]]+)");
    }

    internal static class Encoder {
        public static string EncodeText (string enc) => enc.Replace ("&", "&amp;").Replace ("[", "&#91;").Replace ("]", "&#93;");
        public static string EncodeValue (string text) => EncodeText (text).Replace (",", "&#44;");
        public static string Decode (string enc) => enc.Replace ("&#91;", "[")
            .Replace ("&#93;", "]").Replace ("&#44;", ",").Replace ("&amp;", "&");
    }
}