using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LokiLogger.LoggerAdapter;
using LokiLogger.Shared;

namespace LokiLogger {
	public static class Loki {
		private static Dictionary<ILogAdapter,List<LogLevel>> _adapters { get; set; }
		/// <summary>
		/// Project Name Space Start everything before that ist remove from ClassPath
		/// It is setted automatically but sometimes this is buggy, so set it manually
		/// </summary>
		public static LogLevel DefaultReturnLogLevel { get; set; } = LogLevel.Debug;
		public static LogLevel DefaultInvokeLogLevel { get; set; } = LogLevel.Information;
		public static LogLevel DefaultExceptionLogLevel { get; set; } = LogLevel.Error;





		public const string INFO_TYP = "INF";
		public const string INVOKE_TYP = "INV";
		public const string RETURN_TYP = "RET";
		public const string EXCEPTION_TYP = "EXC";
		public const string REST_TYP = "REC";
		
		
		static Loki()
		{
			_adapters = new Dictionary<ILogAdapter, List<LogLevel>>();
		}

		#region Return


		public static void Return(object data, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(RETURN_TYP,LogLevel.Debug,null,callerName,className,lineNr,data);
		}
		
		public static T Return<T>(T data, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(RETURN_TYP,LogLevel.Debug,null,callerName,className,lineNr,data);
			return data;
		}
		
		public static T Return<T>(string message,T data, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(RETURN_TYP,LogLevel.Debug,message,callerName,className,lineNr,data);
			return data;
		}
		

		#endregion
		
		
		#region Debug

		public static void ExceptionDebug(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Debug, message, callerName, className, lineNr,exception);
		}
		
		public static void ExceptionDebug(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Debug, message, callerName, className, lineNr,exception,obj);
		}
		public static void ExceptionDebug(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Debug, null, callerName, className, lineNr,exception);
		}
		
		
		public static void Debug(string message,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP, LogLevel.Debug,message,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Debug(string message,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Debug,message,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Debug(string message,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Debug,message,callerName,className,lineNr,ob1);
		}
		
		public static void Debug(string message, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Debug,message,callerName,className,lineNr);
		}
		#endregion

		#region Information
		
		public static void ExceptionInformation(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP,LogLevel.Information, message, callerName, className, lineNr,exception);
		}
		public static void ExceptionInformation(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP,LogLevel.Information, message, callerName, className, lineNr,exception,obj);
		}
		
		public static void ExceptionInformation(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP,LogLevel.Information, null, callerName, className, lineNr,exception);

		}
		
		public static void Information(string message,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Information,message,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Information(string message,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Information,message,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Information(string message,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Information,message,callerName,className,lineNr,ob1);
		}
		
		public static void Information(string message, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Information,message,callerName,className,lineNr);
		}
		#endregion

		#region Warning
		public static void ExceptionWarning(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Warning, message, callerName, className, lineNr,exception);
		}
		
		public static void ExceptionWarning(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP,LogLevel.Warning, message, callerName, className, lineNr,exception, obj);
		}
		
		public static void ExceptionWarning(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP,LogLevel.Warning, null, callerName, className, lineNr,exception);
		}

	
		public static void Warning(string message,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Warning,message,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Warning(string message,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Warning,message,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Warning(string message,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Warning,message,callerName,className,lineNr,ob1);
		}
		
		public static void Warning(string message, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Warning,message,callerName,className,lineNr);
		}
		#endregion

		#region Error
		public static void ExceptionError(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Error, message, callerName, className, lineNr,exception);
		}
		
		public static void ExceptionError(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Error, message, callerName, className, lineNr,exception,obj);
		}
		
		public static void ExceptionError(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Error, null, callerName, className, lineNr,exception);
		}

		
		
		public static void Error(string message,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Error,message,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Error(string message,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Error,message,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Error(string message,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Error,message,callerName,className,lineNr,ob1);
		}
		
		public static void Error(string message, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Error,message,callerName,className,lineNr);
		}
		#endregion

		#region Critical
		public static void ExceptionCritical(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Critical, message, callerName, className, lineNr,exception);
		}

		
		public static void ExceptionCritical(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Critical, message, callerName, className, lineNr,exception,obj);
		}
		
		public static void ExceptionCritical(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			Write(EXCEPTION_TYP, LogLevel.Critical, null, callerName, className, lineNr,exception);
		}
		
		public static void Critical(string message,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Critical,message,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Critical(string message,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Critical,message,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Critical(string message,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Critical,message,callerName,className,lineNr,ob1);
		}
		
		public static void Critical(string message, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(INFO_TYP,LogLevel.Critical,message,callerName,className,lineNr);
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

		public static void Write(string typ, LogLevel logLevel,string message, string methodName, string className, int line,
			params object[] data)
		{
			foreach (ILogAdapter logAdapter in _adapters.Keys)
			{
				try
				{
					if (logAdapter == null) continue;
					if(!_adapters[logAdapter].Contains(logLevel))
						logAdapter.Write(typ,logLevel,message,className, methodName,line,data);
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