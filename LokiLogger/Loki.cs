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
		public static string ProjectNameSpace { get; set; }

		public static LogLevel DefaultReturnLogLevel { get; set; } = LogLevel.Debug;
		public static LogLevel DefaultInvokeLogLevel { get; set; } = LogLevel.Information;
		public static LogLevel DefaultExceptionLogLevel { get; set; } = LogLevel.Error;
		static Loki()
		{
			_adapters = new Dictionary<ILogAdapter, List<LogLevel>>();
			ProjectNameSpace = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault()?.Namespace;
		}
		
		
		#region Debug

		public static void ExceptionDebug(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Debug, exception, callerName, className, lineNr,message);
		}
		
		public static void ExceptionDebug(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Debug, exception, callerName, className, lineNr,message,obj);
		}
		public static void ExceptionDebug(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Debug, exception, callerName, className, lineNr);
		}
		
		public static void Return(object data, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteReturn(data,callerName,className,lineNr);
		}
		
		public static T Return<T>(T data, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteReturn(data,callerName,className,lineNr);
			return data;
		}
		
		public static T Return<T>(string message,T data, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteReturn(data,callerName,className,lineNr,message);
			return data;
		}
		
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
		
		public static void ExceptionInformation(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Information, exception, callerName, className, lineNr,message);
		}
		public static void ExceptionInformation(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Information, exception, callerName, className, lineNr,message,obj);
		}
		
		public static void ExceptionInformation(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Information, exception, callerName, className, lineNr);
		}
		
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
		public static void ExceptionWarning(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Warning, exception, callerName, className, lineNr,message);
		}
		
		public static void ExceptionWarning(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Warning, exception, callerName, className, lineNr,message,obj);
		}
		
		public static void ExceptionWarning(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Warning, exception, callerName, className, lineNr);
		}

	
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
		public static void ExceptionError(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Error, exception, callerName, className, lineNr,message);
		}
		
		public static void ExceptionError(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Error, exception, callerName, className, lineNr,message,obj);
		}
		
		public static void ExceptionError(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Error, exception, callerName, className, lineNr);
		}

		
		
		public static void Error(string data,object ob1,object obj2,object obj3, [CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Error,data,callerName,className,lineNr,ob1,obj2,obj3);
		}
		
		public static void Error(string data,object ob1,object obj2,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Error,data,callerName,className,lineNr,ob1,obj2);
		}
		
		public static void Error(string data,object ob1,[CallerLineNumber] int lineNr = 0, [CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Error,data,callerName,className,lineNr,ob1);
		}
		
		public static void Error(string data, [CallerLineNumber] int lineNr = 0,[CallerMemberName] string callerName = "",[CallerFilePath] string className = "")
		{
			Write(LogLevel.Error,data,callerName,className,lineNr);
		}
		#endregion

		#region Critical
		public static void ExceptionCritical(string message, Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Critical, exception, callerName, className, lineNr,message);
		}

		
		public static void ExceptionCritical(string message, Exception exception,object obj, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Critical, exception, callerName, className, lineNr,message,obj);
		}
		
		public static void ExceptionCritical(Exception exception, [CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			WriteException(LogLevel.Critical, exception, callerName, className, lineNr);
		}
		
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
				string[] tmp = className.Split(new string[] { ProjectNameSpace }, StringSplitOptions.None);
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

		public static void WriteInvoke(string methodName, string className, params object[] data)
		{
			string classPath = className;

			if (ProjectNameSpace != null)
			{
				//Yeah hate me, but i like tmp as Name
				string[] tmp = className.Split(new string[] { ProjectNameSpace }, StringSplitOptions.None);
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
					if(!_adapters[logAdapter].Contains(DefaultInvokeLogLevel))
						logAdapter.WriteInvoke(DefaultInvokeLogLevel, methodName, className, data);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
		
		
		public static void WriteReturn(object data, string methodName, string className, int lineNr, string message = null,long elapsedTime = 0)
		{
			string classPath = className;

			if (ProjectNameSpace != null)
			{
				//Yeah hate me, but i like tmp as Name
				string[] tmp = className.Split(new string[] { ProjectNameSpace }, StringSplitOptions.None);
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
					if(!_adapters[logAdapter].Contains(DefaultReturnLogLevel))
						logAdapter.WriteReturn(DefaultReturnLogLevel,message,className, methodName,lineNr,data,elapsedTime);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		public static void WriteException(Exception exception, string methodName, string className, int lineNr,
			string message = null, params object[] data)
		{
			WriteException(DefaultExceptionLogLevel,exception,methodName,className,lineNr,message,data);
		}
		
		private static void WriteException(LogLevel logLevel, Exception exception, string methodName, string className, int lineNr, string message = null,params object[] data)
		{
			string classPath = className;

			if (ProjectNameSpace != null)
			{
				//Yeah hate me, but i like tmp as Name
				string[] tmp = className.Split(new string[] { ProjectNameSpace }, StringSplitOptions.None);
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
						logAdapter.WriteException(logLevel,message,className, methodName,lineNr,exception,data);
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