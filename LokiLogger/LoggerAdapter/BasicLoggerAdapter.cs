using System;
using System.Collections.Generic;
using System.Linq;
using LokiLogger.Model;
using Newtonsoft.Json;

namespace LokiLogger.LoggerAdapter {
	public class BasicLoggerAdapter : ILogAdapter{
		
		public void Write(LogLevel loglvl, string message, string className, string methodName, int line,
			params object[] objects)
		{
			string result = className.Split("/").Last() + "." + methodName + ":" + line;

			switch (loglvl)
			{
				case LogLevel.Verbose:
					result += "[VER]";
					break;
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Critical:
					result += "[CRT]";
					break;
				case LogLevel.SystemCritical:
					result += "[SCRT]";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(loglvl), loglvl, null);
			}

			try
			{
				result += ": " + String.Format(message);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR in String Format with Message " + e.Message);
			}
			
			Console.WriteLine(result);
		}

		public void WriteReturn(LogLevel loglvl, string message, string className, string methodName, int line, object data)
		{
			string result = className.Split("/").Last() + "." + methodName + ":" + line;

			switch (loglvl)
			{
				case LogLevel.Verbose:
					result += "[VER]";
					break;
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Critical:
					result += "[CRT]";
					break;
				case LogLevel.SystemCritical:
					result += "[SCRT]";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(loglvl), loglvl, null);
			}

			result += "[RET]";

			try
			{
				result += ": " + message + " " + JsonConvert.SerializeObject(data);
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR in String Format with Message " + e.Message);
			}
			
			Console.WriteLine(result);
		}

		public void WriteException(LogLevel loglvl, string message, string className, string methodName, int line,
			Exception exception, params object[] data)
		{
			string result = className.Split("/").Last() + "." + methodName + ":" + line;

			switch (loglvl)
			{
				case LogLevel.Verbose:
					result += "[VER]";
					break;
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Critical:
					result += "[CRT]";
					break;
				case LogLevel.SystemCritical:
					result += "[SCRT]";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(loglvl), loglvl, null);
			}

			result += "[EXC]";
			
			try
			{
				
				result += ": " + message + " " + exception.Message + " " + exception.StackTrace;
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR in String Format with Message " + e.Message);
			}
			
			Console.WriteLine(result);
		}
	}
}