using System;
using System.Collections.Generic;
using LokiLogger.Model;

namespace LokiLogger.LoggerAdapter {
	public interface ILogAdapter {
		void Write(LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects);
		void WriteReturn(LogLevel logLevel, string message, string className, string methodName, int lineNr, object data);
		void WriteException(LogLevel logLevel, string message, string className, string methodName, int lineNr, Exception exception,params object[] data);
	}
}