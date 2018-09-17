using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LokiLogger.Model;
using LokiLogger.Writers;

namespace LokiLogger {
	public static class Log {
		static Log()
		{
			_writer = new ConsoleWriter();
		}

		private static Writer _writer { get; set; }
		private static List<LogType> _ignoreList = new List<LogType>();
		
		public static void SetWriter(Writer writer)
		{
			_writer = writer;
		}

		public static void IgnoreType(LogType type)
		{
			_ignoreList.Add(type);
		}
		
		public static void DeIgnoreType(LogType type)
		{
			_ignoreList.Remove(type);
		}



		public static void Verbose(string data, [CallerMemberName] string callerName = "",
			[CallerFilePath] string className = "", [CallerLineNumber] int lineNr = 0)
		{
			LogEvent(LogType.Verbose, data, callerName, className, lineNr);
		}

		public static void Debug(string data, [CallerMemberName] string callerName = "",
			[CallerFilePath] string className = "", [CallerLineNumber] int lineNr = 0)
		{
			LogEvent(LogType.Debug, data, callerName, className, lineNr);
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
			if(!_ignoreList.Contains(type))
			_writer.WriteLog(new Model.Log
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