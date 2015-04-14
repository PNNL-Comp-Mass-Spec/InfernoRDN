using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DAnTE.Tools
{
    public static class clsRCmdLog
    {
        private static bool _cmdLogEnabled = true;
        private static bool _commentLogEnabled = true;

        // log file
        //private static readonly string LogPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static readonly string LogPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string LogAppPath = Path.Combine(LogPath, "Inferno");
        private static string LogFilePath = Path.Combine(LogAppPath, "rcmdlog.txt");
        private static TextWriter _logwriter = null;

        // for stack tracing
        private static bool _traceEnabled = true;
        private static int _traceFramesToShow = 4;
        private const int TraceStartingFrame = 3; // we don't care about internal details


        public static int TraceFramesToShow
        {
            get { return _traceFramesToShow; }
            set { _traceFramesToShow = value; }
        }

        public static bool EnableTrace
        {
            get { return _traceEnabled; }
            set { _traceEnabled = value; }
        }

        public static bool EnableCommentLog
        {
            get { return _commentLogEnabled; }
            set { _commentLogEnabled = value; }
        }

        public static bool EnableCmdLog
        {
            get { return _cmdLogEnabled; }
            set { _cmdLogEnabled = value; }
        }

        public static void Init()
        {
        }

        private static void CreateNewLogFile(FileInfo fiLogFile)
        {
            try
            {
                // Uniquify LogFilePath
                int append = 0;

                while (fiLogFile.Directory != null)
                {
                    append += 1;
                    var newFilePath = Path.Combine(fiLogFile.Directory.FullName,
                                                   Path.GetFileNameWithoutExtension(fiLogFile.Name) + append +
                                                   fiLogFile.Extension);

                    if (!File.Exists(newFilePath))
                    {
                        LogFilePath = newFilePath;
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
            if (_logwriter == null)
            {
                if (!Directory.Exists(LogAppPath))
                {
                    Directory.CreateDirectory(LogAppPath);
                }
                else
                {
                    var fiLogFile = new FileInfo(LogFilePath);

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

                _logwriter = File.AppendText(LogFilePath);
            }
        }
     
        public static void LogOperation(String message)
        {
            if (_cmdLogEnabled)
            {
                EstablishLogWriter();
                if (_logwriter != null)
                {
                    _logwriter.WriteLine("#-------------------------------");
                    _logwriter.WriteLine("# {0}", message);
                    _logwriter.Flush();
                }
            }
            Console.WriteLine("# --- {0}", message);
        }

        public static void LogRCommand(string rcmd)
        {
            var trace = (_traceEnabled) ? "\n" + GetCallingSequence() : "";
            if (_cmdLogEnabled)
            {
                EstablishLogWriter();
                if (_logwriter != null)
                {
                    _logwriter.WriteLine(trace);
                    _logwriter.WriteLine(rcmd);
                    _logwriter.Flush();
                }
            }
            Console.WriteLine(trace);
            Console.WriteLine(rcmd);
        }

        public static void LogRComment(string comment)
        {
            if (_commentLogEnabled)
            {
                EstablishLogWriter();
                if (_logwriter != null)
                {
                    _logwriter.WriteLine("# {0}", comment);
                    _logwriter.Flush();
                }
            }
            Console.WriteLine(string.Format("# {0}", comment));
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
            int lastFrame = TraceStartingFrame + TraceFramesToShow;
            var fc = (frameCount < TraceFramesToShow) ? frameCount : lastFrame;

            var sb = new StringBuilder("# trace: ");

            for (var i = TraceStartingFrame; i < fc; i++)
            {
                var methodBase = stackTrace.GetFrame(i).GetMethod();
                var Class = methodBase.ReflectedType;
                sb.Append(" | ");
                sb.Append(Class.Namespace);
                sb.Append(".");
                sb.Append(Class.Name);
                sb.Append(".");
                sb.Append(methodBase.Name);
            }
            return sb.ToString();
        }

    }
}
