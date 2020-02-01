using System;
using System.Collections.Generic;
using System.IO;
using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger {
        static Dictionary<string, Logger> loggers = new Dictionary<string, Logger> ();
        /// <summary>
        /// 获取日志对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Logger GetLogger (string name) {
            if (loggers.ContainsKey (name) == false)
                loggers.Add (name, new Logger ());
            return loggers[name];
        }
        private Logger () { }
        /// <summary>
        /// 设置日志记录等级
        /// </summary>
        public Verbosity log_level = Verbosity.WARN;
        /// <summary>
        /// 日志输出方式
        /// </summary>
        public LogType log_type = LogType.console;
        static void LogToConsole (
            string message,
            ConsoleColor textColor
        ) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = textColor;
            Console.WriteLine (message);
            Console.ResetColor ();

            Console.OutputEncoding = System.Text.Encoding.Default;
        }
        static void LogToConsole (
            string message,
            ConsoleColor textColor,
            ConsoleColor backColor
        ) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backColor;
            Console.Write (message);
            Console.ResetColor ();
            Console.WriteLine ();

            Console.OutputEncoding = System.Text.Encoding.Default;
        }
        static void LogToConsole (Verbosity v, string message) {
            switch (v) {
            case Verbosity.DEBUG:
                LogToConsole (message,
                    ConsoleColor.Black, ConsoleColor.DarkYellow);
                break;
            case Verbosity.INFO:
                LogToConsole (message, ConsoleColor.Cyan);
                break;
            case Verbosity.WARN:
                LogToConsole (message, ConsoleColor.Yellow);
                break;
            case Verbosity.ERROR:
                LogToConsole (message, ConsoleColor.Red);
                break;
            case Verbosity.FATAL:
                LogToConsole (message, ConsoleColor.Gray, ConsoleColor.Red);
                break;
            }
        }

        private static readonly object _sync = new object ();
        static void LogToFile (Verbosity v, string message) {
            string directory = Path.Combine (Directory.GetCurrentDirectory (), "Log");
            string path = Path.Combine (directory, DateTime.Now.ToString ("yyyy-MM-dd") + ".txt");
            lock (_sync) {
                if (!Directory.Exists (directory)) {
                    Directory.CreateDirectory (directory);
                }
                File.AppendAllText (path, message, System.Text.Encoding.UTF8);
            }
        }
        /// <summary></summary>
        void Log (Verbosity v, string message) {
            if (v > log_level)
                return;
            if ((log_type & LogType.console) != 0)
                LogToConsole (v, $"[{DateTime.Now.ToString("HH:mm:ss")}] [{v.ToString()}] {message}");
            if ((log_type & LogType.file) != 0)
                LogToFile (v, $"[{DateTime.Now.ToString("HH:mm:ss")}] [{v.ToString()}] {message}\r\n");
            LogEvent?.Invoke (v, message);
        }
        ///
        public void Debug (string message) =>
            Log (Verbosity.DEBUG, message);
        ///
        public void Info (string message) =>
            Log (Verbosity.INFO, message);
        ///
        public void Warn (string message) =>
            Log (Verbosity.WARN, message);
        ///
        public void Error (string message) =>
            Log (Verbosity.ERROR, message);
        ///
        public void Fatal (string message) =>
            Log (Verbosity.FATAL, message);

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="v">日志等级</param>
        /// <param name="message">消息</param>
        public delegate void LogAction (Verbosity v, string message);

        /// <summary>
        /// 日志记录事件
        /// </summary>
        public static event LogAction LogEvent;
        //private static LogAction logAction = LogToConsole;
    }

}