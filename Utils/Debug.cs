using System;
using System.Runtime.CompilerServices;
using DSharpPlus.EventArgs;

namespace AchievementTracker.Utils
{
    /// <summary>
    /// Global helper class for debugging the application.
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// The message logging level.
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// Represents an error in the log.
            /// </summary>
            Error,
            /// <summary>
            /// Represents a warning in the log.
            /// </summary>
            Warn,
            /// <summary>
            /// Represents a general info message in the log.
            /// </summary>
            Info,
            /// <summary>
            /// Represents a debug message in the log.
            /// </summary>
            Debug
        }

        private static void SetConsoleColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        /// <summary>
        /// Outputs a message to the console. Use one of the other
        /// available logging functions to simplify your messages.
        /// </summary>
        /// <param name="message">The message to output.</param>
        /// <param name="logLevel">The logging level.</param>
        /// <param name="memberName">The member name that called this function. This gets auto-filled.</param>
        /// <param name="filePath">The file that called this function. This gets auto-filled.</param>
        /// <param name="lineNumber">The line number where this function was called. This gets auto-filled.</param>
        public static void Log(string message, LogLevel logLevel, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            Console.ResetColor();
            Console.Write("[{0:yyyy-MM-dd HH:mm:ss zzz}] ", DateTime.Now.ToLocalTime());
            Console.Write("[");
            SetConsoleColor(logLevel);
            Console.Write(logLevel.ToString().ToUpper());
            Console.ResetColor();
            Console.Write("] ");
            if (Startup.Config != null && Startup.Config.DebugMode)
            {
                Console.Write($"[{memberName}] ");
            }
            SetConsoleColor(logLevel);
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints a debug message to the console.
        /// </summary>
        /// <param name="message">The message to output.</param>
        public static void Print(string message) => Log(message, LogLevel.Debug);

        /// <summary>
        /// Prints an informational message to the console.
        /// </summary>
        /// <param name="message">The message to output.</param>
        public static void Info(string message) => Log(message, LogLevel.Info);

        /// <summary>
        /// Prints a warning message to the console.
        /// </summary>
        /// <param name="message">The message to output.</param>
        public static void Warn(string message) => Log(message, LogLevel.Warn);

        /// <summary>
        /// Prints an error message to the console.
        /// </summary>
        /// <param name="message">The message to output.</param>
        public static void Error(string message) => Log(message, LogLevel.Error);

        /// <summary>
        /// Prints an exception error message to the console.
        /// </summary>
        /// <param name="ex">The exception to output.</param>
        public static void Error(Exception ex) => Log(ex.Message, LogLevel.Error);

        /// <summary>
        /// Event delegate for <see cref="DSharpPlus.DebugLogger"/>.
        /// </summary>
        /// <param name="sender">The object that called this event.</param>
        /// <param name="e">See <see cref="DebugLogMessageEventArgs"/>.</param>
        public static void LogMessageReceived(object sender, DebugLogMessageEventArgs e)
        {
            switch (e.Level)
            {
                case DSharpPlus.LogLevel.Critical:
                case DSharpPlus.LogLevel.Error:
                    Error(e.Message);
                    break;
                case DSharpPlus.LogLevel.Warning:
                    Warn(e.Message);
                    break;
                case DSharpPlus.LogLevel.Debug:
                    Print(e.Message);
                    break;
                case DSharpPlus.LogLevel.Info:
                default:
                    Info(e.Message);
                    break;
            }
        }
    }
}
