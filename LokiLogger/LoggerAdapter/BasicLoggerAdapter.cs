using System;
using System.Collections.Generic;
using System.Linq;
using LokiLogger.Shared;

namespace LokiLogger.LoggerAdapter {
	public class BasicLoggerAdapter : ILogAdapter{
		
		public void Write(string typ, LogLevel loglvl, string message, string className, string methodName, int line,
			params object[] objects)
		{
			string result = className.Split('/').Last() + "." + methodName + ":" + line;

			switch (loglvl)
			{
				case LogLevel.SystemGenerated:
					result += "[SGE]";
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
				case LogLevel.Error:
					result += "[CRT]";
					break;
				case LogLevel.Critical:
					result += "[SCRT]";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(loglvl), loglvl, null);
			}

			result += typ;

			try
			{
				result += ": " + message;
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR in String Format with Message " + e.Message);
			}
			try
			{
				result += " - " + objects;
				
			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR in String Format with Message " + e.Message);
			}
			
			
			Console.WriteLine(result);
		}
	}
}