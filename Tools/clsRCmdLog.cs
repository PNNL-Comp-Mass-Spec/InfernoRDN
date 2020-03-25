using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace DAnTE.Tools
{
    public static class clsRCmdLog
    {
        // log file
        //private static readonly string LogPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static readonly string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string LogAppPath = Path.Combine(LogPath, "Inferno");
        private static TextWriter _logWriter;

        // for stack tracing
        private const int TraceStartingFrame = 3; // we don't care about internal details

        public static string CurrentLogFilePath { get; private set; } = Path.Combine(LogAppPath, "rcmdlog.txt");

        public static int TraceFramesToShow { get; set; } = 4;

        public static bool EnableTrace { get; set; } = true;

        public static bool EnableCommentLog { get; set; } = true;

        public static bool EnableCmdLog { get; set; } = true;

        public static void Init()
        {
        }

        private static void CreateNewLogFile(FileInfo fiLogFile)
        {
            try
            {
                // Uniquify LogFilePath
                var append = 0;

                while (fiLogFile.Directory != null)
                {
                    append += 1;
                    var newFilePath = Path.Combine(fiLogFile.Directory.FullName,
                                                   Path.GetFileNameWithoutExtension(fiLogFile.Name) + append +
                                                   fiLogFile.Extension);

                    if (!File.Exists(newFilePath))
                    {
                        CurrentLogFilePath = newFilePath;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("# Error in CreateNewLogFile: {0}", ex.Message);
            }
        }

        private static void EstablishLogWriter()
        {
            if (_logWriter == null)
            {
                if (!Directory.Exists(LogAppPath))
                {
                    Directory.CreateDirectory(LogAppPath);
                }
                else
                {
                    var fiLogFile = new FileInfo(CurrentLogFilePath);

                    try
                    {
                        if (fiLogFile.Exists)
                        {
                            fiLogFile.Delete();
                        }
                    }
                    catch (IOException ex)
                    {
                        // Could not delete the file
                        // It's possible another copy of Inferno is already running

                        Console.WriteLine("# WARNING: Could not clear log file': {0}", ex.Message);

                        CreateNewLogFile(fiLogFile);
                    }
                }

                _logWriter = File.AppendText(CurrentLogFilePath);
            }
        }


        public static string GetProgramVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static void LogOperation(string message)
        {
            if (EnableCmdLog)
            {
                EstablishLogWriter();
                if (_logWriter != null)
                {
                    _logWriter.WriteLine("#-------------------------------");
                    _logWriter.WriteLine("# {0}", message);
                    _logWriter.Flush();
                }
            }
            Console.WriteLine("# --- {0}", message);
        }

        public static void LogRCommand(string rcmd)
        {
            var trace = (EnableTrace) ? "\n" + GetCallingSequence() : "";
            if (EnableCmdLog)
            {
                EstablishLogWriter();
                if (_logWriter != null)
                {
                    _logWriter.WriteLine(trace);
                    _logWriter.WriteLine(rcmd);
                    _logWriter.Flush();
                }
            }
            Console.WriteLine(trace);
            Console.WriteLine(rcmd);
        }

        public static void LogRComment(string comment)
        {
            if (EnableCommentLog)
            {
                EstablishLogWriter();
                if (_logWriter != null)
                {
                    _logWriter.WriteLine("# {0}", comment);
                    _logWriter.Flush();
                }
            }
            Console.WriteLine("# {0}", comment);
        }

        /// <summary>
        /// Return the immediate calling sequence from the stack
        /// Caution: very expensive, enable only when debugging
        /// </summary>
        /// <returns></returns>
        private static string GetCallingSequence()
        {
            var stackTrace = new StackTrace();
            var frameCount = stackTrace.FrameCount;
            var lastFrame = TraceStartingFrame + TraceFramesToShow;
            var fc = (frameCount < TraceFramesToShow) ? frameCount : lastFrame;

            var sb = new StringBuilder("# trace: ");

            for (var i = TraceStartingFrame; i < fc; i++)
            {
                var methodBase = stackTrace.GetFrame(i).GetMethod();
                var Class = methodBase.ReflectedType;
                sb.Append(" | ");
                if (Class != null)
                {
                    sb.Append(Class.Namespace);
                    sb.Append(".");
                    sb.Append(Class.Name);
                }
                sb.Append(".");
                sb.Append(methodBase.Name);
            }
            return sb.ToString();
        }
    }
}