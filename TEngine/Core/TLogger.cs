using System.Diagnostics;
using System.Text;

namespace TEngine
{
    public static class ColorUtils
    {
        #region ColorStr

        public const string White = "FFFFFF";
        public const string Black = "000000";
        public const string Red = "FF0000";
        public const string Green = "00FF18";
        public const string Oringe = "FF9400";
        public const string Exception = "FF00BD";
        #endregion

        public static string ToColor(this string str, string color)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return string.Format("<color=#{0}>{1}</color>", color, str);
        }
    }

    public class TLogger : TSingleton<TLogger>
    {
        protected override void Init()
        {
            _outputType = OutputType.EDITOR;
        }

        ~TLogger()
        {

        }

        public override void Release()
        {
            base.Release();
        }


        private void ChangeOutputChannel(OutputType type)
        {
            if (type != _outputType)
            {
                _outputType = type;
            }
        }

        public static void SetFilterLevel(LogLevel filterLevel)
        {
            Instance._filterLevel = filterLevel;
        }

        public static void LogAssert(bool condition, string logStr)
        {
            if (!condition)
                Instance.Log(LogLevel.ASSERT, logStr);
        }

        public static void LogAssert(bool condition, string format, params System.Object[] args)
        {
            if (!condition)
            {
                string logStr = string.Format(format, args);
                Instance.Log(LogLevel.ASSERT, logStr);
            }
        }

        public static void LogInfo(string logStr)
        {
            Instance.Log(LogLevel.INFO, logStr);
        }

        public static void LogInfo(string format, params System.Object[] args)
        {
            string logStr = string.Format(format, args);
            Instance.Log(LogLevel.INFO, logStr);
        }

        public static void LogInfoSuccessd(string logStr)
        {
            Instance.Log(LogLevel.Successd, logStr);
        }

        public static void LogInfoSuccessd(string format, params System.Object[] args)
        {
            string logStr = string.Format(format, args);
            Instance.Log(LogLevel.Successd, logStr);
        }

        public static void LogWarning(string logStr)
        {
            Instance.Log(LogLevel.WARNING, logStr);
        }

        public static void LogWarning(string format, params System.Object[] args)
        {
            string logStr = string.Format(format, args);
            Instance.Log(LogLevel.WARNING, logStr);
        }

        public static void LogError(string logStr)
        {
            Instance.Log(LogLevel.ERROR, logStr);
        }

        public static void LogError(string format, params System.Object[] args)
        {
            string logStr = string.Format(format, args);
            Instance.Log(LogLevel.ERROR, logStr);
        }

        public static void LogException(string logStr)
        {
            Instance.Log(LogLevel.EXCEPTION, logStr);
        }

        public static void LogException(string format, params System.Object[] args)
        {
            string msg = string.Format(format, args);
            Instance.Log(LogLevel.EXCEPTION, msg);
        }

        private StringBuilder GetFormatedString(LogLevel logLevel, string logString, bool bColor)
        {
            _stringBuilder.Clear();
            switch (logLevel)
            {
                case LogLevel.Successd:
                    if (UseCustomColor)
                    {
                        _stringBuilder.AppendFormat("[TLogger][SUCCESSED][{0}] - <color=#{2}>{1}</color>",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString, ColorUtils.Green);
                    }
                    else
                    {
                        _stringBuilder.AppendFormat(
                            bColor ? "[TLogger][INFO][{0}] - <color=gray>{1}</color>" : "[TLogger][SUCCESSED][{0}] - {1}",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString);
                    }
                    break;
                case LogLevel.INFO:
                    if (UseCustomColor)
                    {
                        _stringBuilder.AppendFormat("[TLogger][INFO][{0}] - <color=#{2}>{1}</color>",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString, ColorUtils.Black);
                    }
                    else
                    {
                        _stringBuilder.AppendFormat(
                            bColor ? "[TLogger][INFO][{0}] - <color=gray>{1}</color>" : "[TLogger][INFO][{0}] - {1}",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString);
                    }
                    break;
                case LogLevel.ASSERT:
                    if (UseCustomColor)
                    {
                        _stringBuilder.AppendFormat("[TLogger][ASSERT][{0}] - <color=#{2}>{1}</color>",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString, ColorUtils.Exception);
                    }
                    else
                    {
                        _stringBuilder.AppendFormat(
                            bColor ? "[TLogger][ASSERT][{0}] - <color=green>{1}</color>" : "[TLogger][ASSERT][{0}] - {1}",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString);
                    }
                    break;
                case LogLevel.WARNING:
                    if (UseCustomColor)
                    {
                        _stringBuilder.AppendFormat("[TLogger][WARNING][{0}] - <color=#{2}>{1}</color>",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString, ColorUtils.Oringe);
                    }
                    else
                    {
                        _stringBuilder.AppendFormat(
                            bColor
                                ? "[TLogger][WARNING][{0}] - <color=yellow>{1}</color>"
                                : "[TLogger][WARNING][{0}] - {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                            logString);
                    }
                    break;
                case LogLevel.ERROR:
                    if (UseCustomColor)
                    {
                        _stringBuilder.AppendFormat("[ERROR][WARNING][{0}] - <color=#{2}>{1}</color>",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString, ColorUtils.Red);
                    }
                    else
                    {
                        _stringBuilder.AppendFormat(
                            bColor ? "[TLogger][ERROR][{0}] - <color=red>{1}</color>" : "[TLogger][ERROR][{0}] - {1}",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString);
                    }
                    break;
                case LogLevel.EXCEPTION:
                    if (UseCustomColor)
                    {
                        _stringBuilder.AppendFormat("[ERROR][EXCEPTION][{0}] - <color=#{2}>{1}</color>",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString, ColorUtils.Exception);
                    }
                    else
                    {
                        _stringBuilder.AppendFormat(
                            bColor
                                ? "[TLogger][EXCEPTION][{0}] - <color=red>{1}</color>"
                                : "[TLogger][EXCEPTION][{0}] - {1}",
                            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"), logString);
                    }
                    break;
            }

            return _stringBuilder;
        }

        private void Log(LogLevel type, string logString)
        {
            if (_outputType == OutputType.NONE)
            {
                return;
            }

            if (type < _filterLevel)
            {
                return;
            }

            StringBuilder infoBuilder = GetFormatedString(type, logString, UseSystemColor);

            if (type == LogLevel.ERROR || type == LogLevel.WARNING || type == LogLevel.EXCEPTION)
            {
                StackFrame[] sf = new StackTrace().GetFrames();
                for (int i = 0; i < sf.Length; i++)
                {
                    StackFrame frame = sf[i];
                    string? declaringTypeName = frame.GetMethod()?.DeclaringType.FullName;
                    string? methodName = frame.GetMethod()?.Name;
                    infoBuilder.AppendFormat("[{0}::{1}\n", declaringTypeName, methodName);
                }
            }
            string logStr = infoBuilder.ToString();

            if (type == LogLevel.INFO)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(logStr);
            }
            else if (type == LogLevel.Successd)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(logStr);
            }
            else if (type == LogLevel.WARNING)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(logStr);
            }
            else if (type == LogLevel.ASSERT)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(logStr);
            }
            else if (type == LogLevel.ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(logStr);
            }
            else if (type == LogLevel.EXCEPTION)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(logStr);
            }
        }

        #region Properties
        public bool UseCustomColor = false;
        public bool UseSystemColor = false;

        public enum LogLevel
        {
            INFO,
            Successd,
            ASSERT,
            WARNING,
            ERROR,
            EXCEPTION,
        }

        [System.Flags]
        public enum OutputType
        {
            NONE = 0,
            EDITOR = 0x1,
            GUI = 0x2,
            FILE = 0x4
        }

        private LogLevel _filterLevel = LogLevel.INFO;
        private OutputType _outputType = OutputType.EDITOR;
        private StringBuilder _stringBuilder = new StringBuilder();
        #endregion
    }
}