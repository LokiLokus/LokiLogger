using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LokiLogger.Model;
using LokiLogger.Writers;

namespace LokiLogger {
	public static class Log {
		private static readonly List<LogType> _ignoreList = new List<LogType>();

		private static List<IWriter> _writer { get; set; }
		static Log()
		{
			_writer = new List<IWriter> {new ConsoleWriter()};
		}


		public static void AddWriter(IWriter writer)
		{
			_writer.Add(writer);
		}

		public static void IgnoreType(LogType type)
		{
			_ignoreList.Add(type);
		}

		public static void DeIgnoreType(LogType type)
		{
			_ignoreList.Remove(type);
		}

		/// <summary>
		///     This Function garants that all Logs are written
		/// </summary>
		/// <returns></returns>
		public static async Task StopLog()
		{
			var tasks = _writer.Select(x => x.Stop());
			await Task.WhenAll(tasks);
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
			if (!_ignoreList.Contains(type))
			{
				Model.Log tmp = new Model.Log
				{
					Message = data,
					Time = DateTime.Now,
					Type = type,
					Class = className,
					Method = method,
					LineNr = lineNr
				};
				_writer.ForEach(x =>
				{
					x.WriteLog(tmp);
				});
			}
				 
		}
	}
}