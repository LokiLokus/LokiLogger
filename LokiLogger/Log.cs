using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LokiLogger.LoggerAdapter;
using LokiLogger.Model;
using LokiLogger.Writers;

namespace LokiLogger {
	public static class Log {
		private static readonly List<LogLevel> _ignoreList = new List<LogLevel>();
		private static List<ILogAdapter> _writer { get; set; }
		
		static Log()
		{
			_writer = new List<ILogAdapter> {new BasicLoggerAdapter()};
		}
		
		#region Verbose
		public static void Verbose(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Verbose,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Verbose(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Verbose,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Verbose(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Verbose,data,callerName,className,lineNr,ob1);
		}
		
		public static void Verbose(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Verbose,data,callerName,className,lineNr);
		}
#endregion
		
		#region Debug
		public static void Debug(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Debug,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Debug(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Debug,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Debug(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Debug,data,callerName,className,lineNr,ob1);
		}
		
		public static void Debug(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Debug,data,callerName,className,lineNr);
		}
		#endregion

		#region Information
		public static void Information(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Information,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Information(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Information,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Information(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Information,data,callerName,className,lineNr,ob1);
		}
		
		public static void Information(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Information,data,callerName,className,lineNr);
		}
		#endregion

		#region Warning
		public static void Warning(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Warning,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Warning(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Warning,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Warning(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Warning,data,callerName,className,lineNr,ob1);
		}
		
		public static void Warning(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Warning,data,callerName,className,lineNr);
		}
		#endregion

		#region Error
		public static void Critical(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Critical,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Critical(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Critical,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Critical(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Critical,data,callerName,className,lineNr,ob1);
		}
		
		public static void Critical(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Critical,data,callerName,className,lineNr);
		}
		#endregion

		#region SystemCritical
		public static void SystemCritical(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.SystemCritical,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void SystemCritical(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.SystemCritical,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void SystemCritical(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.SystemCritical,data,callerName,className,lineNr,ob1);
		}
		
		public static void SystemCritical(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.SystemCritical,data,callerName,className,lineNr);
		}
		#endregion
		
		
		public static void AddWriter(ILogAdapter logAdapter)
		{
			if (logAdapter == null) throw new LoggerAdapterIsNullException();
			_writer.Add(logAdapter);
		}

		public static void IgnoreType(LogLevel level)
		{
			_ignoreList.Add(level);
		}

		public static void DeIgnoreType(LogLevel level)
		{
			_ignoreList.Remove(level);
		}

		private static void Write(LogLevel logLevel, string message, string methodName, string className, int line,
			params object[] objects)
		{
			if(_ignoreList.Contains(logLevel)) return;
			_writer.ForEach(x =>
			{
				x.Write(logLevel,message,className, methodName,line,objects);
			});
		}
	}
	public class LoggerAdapterIsNullException : Exception {
	}
}