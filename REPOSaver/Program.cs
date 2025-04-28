
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using NLog;
using NLog.Config;
using NLog.Fluent;
using NLog.Targets;

namespace REPOSaver
{
    internal static class Program
    {
        public static string RepoVersion { get; private set; } = "";

        public static Logger Log => LogManager.GetLogger(nameof(Program));

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ReadGameVersion();
            SetupLogging();
            int pid = Process.GetCurrentProcess().Id;
            Log.Info("Session Starting at {0} (PID: {1})", DateTime.UtcNow, pid);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
            Log.Info("Session Ending at {0} (PID: {1})", DateTime.UtcNow, pid);
        }

        private static void ReadGameVersion()
        {
            const string RSC_NAME = "REPOSaver.gamever.txt";

            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(RSC_NAME))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    RepoVersion = reader.ReadToEnd().Trim();
                }
            }
        }

        private static void SetupLogging()
        {
            if(!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            string layout = "${longdate} | ${level:uppercase=true} | ${logger} | (${threadid}) ${message} ${exception}";

            var config = new LoggingConfiguration();
            var stdTarget = new ConsoleTarget("stdout") { Layout = layout };
            var debugTarget = new DebuggerTarget("debugger") { Layout = layout };
            var fileTarget = new FileTarget("file");
            fileTarget.FileName = "log_latest.txt";
            fileTarget.ArchiveOldFileOnStartup = true;
            fileTarget.ArchiveFileName = "logs\\log_{####}.txt";
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Sequence;
            fileTarget.AutoFlush = true;
            fileTarget.Layout = layout;

            config.AddTarget(stdTarget);
            config.AddTarget(debugTarget);
            config.AddTarget(fileTarget);

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, stdTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, debugTarget);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }
    }


}