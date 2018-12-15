using System;
using System.Collections.Generic;
using cqhttp.Cyan.Enums;

namespace cqhttp.Cyan {
    /// <summary>
    /// 日志
    /// </summary>
    public class Logger {
        static Verbosity v_setting = Verbosity.WARN;
        /// <summary>
        /// 设置日志记录等级
        /// </summary>
        public static Verbosity verbosity_level {
            set { v_setting = value; }
        }
        static void LogToConsole (
            string message,
            ConsoleColor textColor
        ) {
            Console.ForegroundColor = textColor;
            Console.WriteLine (message);
            Console.ResetColor ();
        }
        static void LogToConsole (
            string message,
            ConsoleColor textColor,
            ConsoleColor backColor
        ) {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backColor;
            Console.Write (message);
            Console.ResetColor ();
            Console.WriteLine ();
        }
        static void LogToConsole (Verbosity v, string message) {
            if (v <= v_setting) {
                switch (v) {
                    case Verbosity.DEBUG:
                        LogToConsole ("[DEBUG] " + message,
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
        }
        static void LogToFile (Verbosity v, string message) {

        }
        /// <summary></summary>
        public static void Log (Verbosity v, string message) {
            logAction (v, message);
        }
        private delegate void LogAction (Verbosity v, string message);
        private static LogAction logAction = LogToConsole;
    }

}