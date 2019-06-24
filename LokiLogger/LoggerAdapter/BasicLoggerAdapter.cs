using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using LokiLogger.Model;
namespace LokiLogger.LoggerAdapter {
	public class BasicLoggerAdapter : ILogAdapter{
		
		public void Write(LogLevel loglvl, string message, string className, string methodName, int line,
			params object[] objects)
		{
			string result = className.Split("/").Last() + "." + methodName + ":" + line;

			switch (loglvl)
			{
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Error:
					result += "[CRT]";
					break;
				case LogLevel.Critical:
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

		public void WriteReturn(LogLevel loglvl, string message, string className, string methodName, int line, object data,long elapsedTime)
		{
			string result = className.Split("/").Last() + "." + methodName + ":" + line;

			switch (loglvl)
			{
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Error:
					result += "[CRT]";
					break;
				case LogLevel.Critical:
					result += "[SCRT]";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(loglvl), loglvl, null);
			}

			result += "[RET]";

			try
			{
				result += ": " + message + " " + data + " " + elapsedTime;
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
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Error:
					result += "[CRT]";
					break;
				case LogLevel.Critical:
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

		public void WriteInvoke(LogLevel logLevel,string methodName, string className, object[] data)
		{
			string result = className.Split("/").Last() + "." + methodName;
			switch (logLevel)
			{
				case LogLevel.Debug:
					result += "[DEB]";
					break;
				case LogLevel.Information:
					result += "[INF]";
					break;
				case LogLevel.Warning:
					result += "[WAR]";
					break;
				case LogLevel.Error:
					result += "[CRT]";
					break;
				case LogLevel.Critical:
					result += "[SCRT]";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
			}

			result += "[INV]";
			
			try
			{

				result += ": " + data;
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR in String Format with Message " + e.Message);
			}
			
			Console.WriteLine(result);
		}
	}
}