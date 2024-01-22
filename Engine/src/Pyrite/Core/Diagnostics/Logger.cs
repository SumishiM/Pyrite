//using System.Runtime.CompilerServices;
//using System.Diagnostics;
//using System.Text;

//namespace Pyrite.Core.Diagnostics
//{
//    public class Logger
//    {
//        protected static Logger? _instance;
//        public static Logger Instance
//        {
//            get
//            {
//                _instance ??= new Logger();
//                return _instance;
//            }
//        }

//        protected readonly IList<LogLine> _log = [];

//        protected readonly struct LogLine
//        {
//            public string Message { get; init; }
//            public Vector4 Color { get; init; }
//            public int Repeats { get; init; }
//        }



//        /// <summary>
//        /// Singleton
//        /// </summary>
//        protected Logger() { }

//        protected void Initialize(bool diagnostic)
//        {
//            if (diagnostic)
//            {
//                AppDomain.CurrentDomain.FirstChanceException += (_, e) =>
//                {
//                    StringBuilder message = new();
//                    message.Append($"Exception was thrown ! {e.Exception.Message}");

//                    if (e.Exception.Source is not string source || !source.Contains("Newtonsoft"))
//                    {
//                        message.Append($"\n{e.Exception.StackTrace}");
//                    }

//                    Warning(message.ToString());
//                };
//            }
//        }

//        private bool CheckRepeat(string message)
//        {
//            if (_log.LastOrDefault().Message == message)
//            {
//                LogLine lastLog = _log.Last();
//                _log[^1] = lastLog with { Repeats = lastLog.Repeats + 1 };
//                return true;
//            }
//            return false;
//        }

//        private void ClearLog() => _log.Clear();

//        public static void LogPerf(string v, Vector4? color = null) =>
//            Instance.LogPerfImpl(v, color ?? new Vector4(1, 1, 1, 1));

//        public static void Log(string message, Color? color = null)
//            => Instance.LogImpl(message, (color ?? Color.White).ToVector4());

//        public static void Warning(
//            string message, 
//            [CallerMemberName] string memberName = "",
//            [CallerLineNumber] int linenumber = 0)
//            => Instance.LogWarningImpl(message, memberName, linenumber);

//        public static void Error(
//            string message,
//            [CallerMemberName] string memberName = "",
//            [CallerLineNumber] int linenumber = 0)
//            => Instance.LogErrorImpl(message, memberName, linenumber);

//        private void LogPerfImpl(string rawMessage, Vector4 color)
//        {
//            if (CheckRepeat(rawMessage))
//            {
//                return;
//            }

//            string message = $"[PERF] 📈 {rawMessage}";
//            Debug.WriteLine(message);

//            OutputToLog($"\uf201 {rawMessage}", color);
//        }

//        private void LogImpl(string rawMessage, Vector4 color)
//        {
//            if (CheckRepeat(rawMessage))
//                return;

//            string message = $"[Log] {rawMessage}";
//            Debug.WriteLine(message);

//            OutputToLog(rawMessage, color);
//        }

//        private void LogWarningImpl(string rawMessage, string memberName, int lineNumber)
//        {
//            if (CheckRepeat(rawMessage))
//                return;

//            string message = $"[Warn] {rawMessage}";
//            Debug.WriteLine(message);

//            string outputMessage = rawMessage;
//            if( !string.IsNullOrEmpty(memberName)  && lineNumber != 0)
//            {
//                outputMessage = $"{memberName} (line {lineNumber}): {outputMessage}";
//            }

//            OutputToLog(rawMessage, new Vector4(1, 1, 0.5f, 1));
//        }

//        private void LogErrorImpl(string rawMessage, string memberName, int lineNumber)
//        {
//            if (CheckRepeat(rawMessage))
//                return;

//            string message = $"[Err] {rawMessage}";
//            Debug.WriteLine(message);

//            string outputMessage = rawMessage;
//            if (!string.IsNullOrEmpty(memberName) && lineNumber != 0)
//            {
//                outputMessage = $"{memberName} (line {lineNumber}): {outputMessage}";
//            }

//            OutputToLog(rawMessage, new Vector4(1, 0.25f, 0.5f, 1));
//        }

//        private void OutputToLog(string message, Vector4 color, bool includeTime = true)
//        {
//            string time = includeTime ? $"[{DateTime.Now:HH:mm:ss}] " : string.Empty;

//            using StringReader reader = new(time + message);

//            for (string? line = reader.ReadLine(); line != null; line = reader.ReadLine())
//            {
//                _log.Add(new LogLine() { Message = line, Color = color, Repeats = 1 });
//            }
//        }
//    }
//}
