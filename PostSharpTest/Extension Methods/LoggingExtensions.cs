using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Console = Colorful.Console;

namespace PostSharpTest.Extension_Methods
{
    /// <summary>
    /// Logging extensions class
    /// </summary>
    public static class LoggingExtensions
    {
        #region Constants

        private const string InfoLog = "infomsg.log";

        #endregion

        #region Private Fields

        private static readonly string FilePath;
        private static object _logLock = new object();
        private static object _dumpLock = new object();
        private static readonly string[] LogLevels;
        private static bool OverwriteLog;
        private static string AppDir;
        private static LogLevels levelSetting = Extension_Methods.LogLevels.None;

        #endregion

        #region Constructor

        /// <summary>
        /// Common constructor used by extension methods.
        /// </summary>
        static LoggingExtensions()
        {
            LogLevels = new[] { "NONE", "INFO", "WARN", "DEBUG", "ERROR" };
            bool tmp;

            // Retrieve the 'Log Info Msg' config setting ...
            var setting = ConfigurationManager.AppSettings["LogInfoMsgs"] ?? "false";
            bool.TryParse(setting, out tmp);

            if (tmp)
            {
                levelSetting = Extension_Methods.LogLevels.Info;
            }

            // Retrieve the 'Log Info Msg' config setting ...
            setting = ConfigurationManager.AppSettings["LogFatalErrors"] ?? "false";
            bool.TryParse(setting, out tmp);

            if (tmp)
            {
                levelSetting = Extension_Methods.LogLevels.Error;
            }

            // Build path to log file ...
            var appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            AppDir = Path.GetDirectoryName(appPath);
            FilePath = Path.Combine(AppDir, InfoLog);

            // Retrieve 'overwrite log' setting ...
            setting = ConfigurationManager.AppSettings["OverwriteLogs"] ?? "false";
            bool.TryParse(setting, out OverwriteLog);

            // Should we overwrite existing log file ? ...
            if (OverwriteLog)
            {
                // Delete log file & swallow any errors ...
                try
                {
                    if (File.Exists(FilePath))
                        File.Delete(FilePath);
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion

        #region Public Extension Methods

        /// <summary>
        /// Logs the supplied message to log file
        /// </summary>
        /// <param name="infoMessage">Message to log</param>
        /// <param name="level">The log level to use for the message. Defaults to Info.</param>
        /// <param name="toConsole">Flag governing if message also written to console</param>
        public static void LogMsg(this string infoMessage,
            LogLevels level = PostSharpTest.Extension_Methods.LogLevels.Info,
            bool toConsole = true)
        {
            // Bail if logging turned off ...
            if (levelSetting == Extension_Methods.LogLevels.None)
                return;

            // Bail if log request above the current logging level ...
            if (level > levelSetting)
                return;

            // Ensure is thread-safe ...
            lock (_logLock)
            {
                // Get current thread ID 
                var currentThreadID = Thread.CurrentThread.ManagedThreadId;

                // Establish class name of calling method ...
                var method = new StackTrace().GetFrame(1).GetMethod();
                var className = method.ReflectedType.Name;

                // Append details of the exception to the log file ...
                using (var writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:sss.fff} - {LogLevels[(int)level]} - <{currentThreadID}> [{className}]\t{infoMessage}");
                }

                // Out to console if required ...
                if (toConsole)
                {
                    Console.WriteLine(infoMessage, LevelToColour(level));
                    Debug.WriteLine(infoMessage);
                }
            }
        }

        /// <summary>
        /// Obsolete method
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="dumpFile"></param>
        public static void DumpFile(this string payload, string dumpFile)
        {
            // Bail if logging turned off ...
            if (levelSetting == Extension_Methods.LogLevels.None)
                return;

            if (string.IsNullOrEmpty(dumpFile))
                return;

            var filePath = Path.Combine(AppDir, dumpFile);

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            catch
            {
            }

            lock (_dumpLock)
            {
                using (var writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{payload}");
                }
            }
        }

        /// <summary>
        /// Obsolete method
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="xmlFile"></param>
        public static void DumpAsXml(this string payload, string xmlFile)
        {
            if (string.IsNullOrEmpty(xmlFile))
                return;

            string formattedXml;

            try
            {
                var doc = XDocument.Parse(payload);
                formattedXml = doc.ToString();
            }
            catch
            {
                formattedXml = payload;
            }

            formattedXml.DumpFile(xmlFile);
        }

        private static Color LevelToColour(LogLevels level)
        {
            Color result;

            switch (level)
            {
                case Extension_Methods.LogLevels.Info:
                    result = Color.Aqua;
                    break;

                case Extension_Methods.LogLevels.Debug:
                    result = Color.DeepPink;
                    break;

                case Extension_Methods.LogLevels.Warn:
                    result = Color.Coral;
                    break;

                case Extension_Methods.LogLevels.Error:
                    result = Color.Red;
                    break;

                default:
                    result = Color.LightGray;
                    break;
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Enumeration listing the supported logging levels
    /// </summary>
    public enum LogLevels : int
    {
        /// <summary>
        /// No logging enabled
        /// </summary>
        None = 0,

        /// <summary>
        /// Information level logging. N.B. This is the default logging level.
        /// </summary>
        Info,

        /// <summary>
        /// Warning logging level. Used to record non-fatal error conditions.
        /// </summary>
        Warn,

        /// <summary>
        /// Debug logging level. Used to include debug-level diagnostic output.
        /// </summary>
        Debug,

        /// <summary>
        /// Error logging level. Used to record exception details and other fatal error conditions.
        /// </summary>
        Error

    }
}
