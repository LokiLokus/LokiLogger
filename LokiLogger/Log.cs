using System;
using System.Runtime.CompilerServices;
using LokiLogger.Model;
using LokiLogger.Writers;

namespace LokiLogger {
	public static class Log {
		static Log()
		{
			Writer = new ConsoleWriter();
		}

		private static Writer Writer { get; set; }

		public static void SetWriter(Writer writer)
		{
			Writer = writer;
		}


		public static void Info(string data, [CallerMemberName] string callerName = "",
			[CallerFilePath] string className = "", [CallerLineNumber] int lineNr = 0)
		{
			LogEvent(LogType.Information, data, callerName, className, lineNr);
		}

		public static void Warn(string data, [CallerMemberName] string callerName = "",
			[CallerFilePath] string className = "", [CallerLineNumber] int lineNr = 0)
		{
			LogEvent(LogType.Warning, data, callerName, className, lineNr);
		}

		public static void Crit(string data, [CallerMemberName] string callerName = "",
			[CallerFilePath] string className = "", [CallerLineNumber] int lineNr = 0)
		{
			LogEvent(LogType.Critical, data, callerName, className, lineNr);
		}

		public static void SysCrit(string data, [CallerMemberName] string callerName = "",
			[CallerFilePath] string className = "", [CallerLineNumber] int lineNr = 0)
		{
			LogEvent(LogType.SystemCritical, data, callerName, className, lineNr);
		}

		private static void LogEvent(LogType type, string data, string method, string className, int lineNr)
		{
			Writer.WriteLog(new Model.Log
			{
				Message = data,
				Time = DateTime.Now,
				Type = type,
				Class = className,
				Method = method,
				LineNr = lineNr
			});
		}
	}
}