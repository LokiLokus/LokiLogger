
using System;
using LokiLogger.Model;
using Serilog;
using Serilog.Events;
using Log = Serilog.Log;

namespace LokiLogger.LoggerAdapter {
	public class SerilogLoggerAdapter : ILogAdapter {
		public SerilogLoggerAdapter()
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console()
				.CreateLogger();

		}
		public void Write(LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects)
		{
			Log.Write(LogLevelToLogEventLevel(logLevel),$"{className}.{methodName}:{line} {message}",objects);
		}
		private LogEventLevel LogLevelToLogEventLevel(LogLevel lvl)
		{
			switch (lvl)
			{
				case LogLevel.Verbose:
					return LogEventLevel.Verbose;
					break;
				case LogLevel.Debug:
					return LogEventLevel.Debug;
					break;
				case LogLevel.Information:
					return LogEventLevel.Information;
					break;
				case LogLevel.Warning:
					return LogEventLevel.Warning;
					break;
				case LogLevel.Critical:
					return LogEventLevel.Error;
					break;
				case LogLevel.SystemCritical:
					return LogEventLevel.Fatal;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(lvl), lvl, null);
			}
		}
	}
}