using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LokiLogger.LoggerAdapter;
using LokiLogger.Model;

namespace LokiLogger {
	public static class Loki {
		private static Dictionary<ILogAdapter,List<LogLevel>> _adapters { get; set; }
		/// <summary>
		/// Project Name Space Start everything before that ist remove from ClassPath
		/// </summary>
		public static string ProjectNameSpace { get; set; }
		static Loki()
		{
			_adapters = new Dictionary<ILogAdapter, List<LogLevel>>();
			UpdateAdapter(new BasicLoggerAdapter());
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
		
		/// <summary>
		/// Adds a Writer to Logger, ignores Nothing
		/// </summary>
		/// <param name="logAdapter"></param>
		/// <exception cref="LoggerAdapterIsNullException"></exception>
		public static void UpdateAdapter(ILogAdapter logAdapter)
		{
			if (logAdapter == null) throw new LoggerAdapterIsNullException();
			if (_adapters.ContainsKey(logAdapter))
			{
				_adapters[logAdapter] = new List<LogLevel>();
			}
			else
			{
				_adapters.Add(logAdapter,new List<LogLevel>());		
			}
		}
		/// <summary>
		/// Updates the Adapter, adds if not already added, Updates IgnoreList for the given Adapter
		/// </summary>
		/// <param name="logAdapter"></param>
		/// <param name="ignoreList"></param>
		/// <exception cref="LoggerAdapterIsNullException"></exception>
		/// <exception cref="IgnoreListIsNullException"></exception>
		public static void UpdateAdapter(ILogAdapter logAdapter,List<LogLevel> ignoreList)
		{
			if(logAdapter == null) throw new ArgumentNullException("logAdapter is null");
			if(ignoreList == null) throw new ArgumentNullException("ignoreList is null");
			if (logAdapter == null) throw new LoggerAdapterIsNullException();
			if (ignoreList == null) throw new IgnoreListIsNullException();
			if (_adapters.ContainsKey(logAdapter))
			{
				_adapters[logAdapter] = ignoreList;
			}
			else
			{
				_adapters.Add(logAdapter,ignoreList);				
			}
		}

		/// <summary>
		/// Remove the Adapter from the subscription List
		/// </summary>
		/// <param name="logAdapter"></param>
		public static void RemoveAdapter(ILogAdapter logAdapter)
		{
			if(logAdapter == null) throw new ArgumentNullException("logAdapter is null");
			if(_adapters.ContainsKey(logAdapter))
				_adapters.Remove(logAdapter);
		}

		/// <summary>
		/// Returns a Dictionary with the ILogAdapter as Key
		/// and their List of ignored LogLevels
		/// </summary>
		/// <returns></returns>
		public static Dictionary<ILogAdapter, List<LogLevel>> GetAllAdapter()
		{
			return _adapters;
		}

		private static void Write(LogLevel logLevel, string message, string methodName, string className, int line,
			params object[] objects)
		{
			string classPath = className;

			if (ProjectNameSpace != null)
			{
			
				//Yeah hate me, but i like tmp as Name
				string[] tmp = className.Split(ProjectNameSpace,2);
				if (tmp.Length > 1)
				{
					className = $"{ProjectNameSpace}{tmp[1]}";
				}
			}
			
			foreach (ILogAdapter logAdapter in _adapters.Keys)
			{
				try
				{
					if (logAdapter == null) continue;
					if(!_adapters[logAdapter].Contains(logLevel))
						logAdapter.Write(logLevel,message,className, methodName,line,objects);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
	public class LoggerAdapterIsNullException : Exception {}
	public class IgnoreListIsNullException : Exception {}
}