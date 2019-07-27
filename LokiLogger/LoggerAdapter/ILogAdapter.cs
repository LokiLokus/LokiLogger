using System;
using System.Collections.Generic;
using LokiLogger.Shared;

namespace LokiLogger.LoggerAdapter {
	public interface ILogAdapter {
		void Write(LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects);
		void WriteReturn(LogLevel logLevel, string message, string className, string methodName, int lineNr, object data,long elapsedTime);
		void WriteException(LogLevel logLevel, string message, string className, string methodName, int lineNr, Exception exception,params object[] data);
		void WriteInvoke(LogLevel logLevel,string methodName, string className, object[] data);
	}
}