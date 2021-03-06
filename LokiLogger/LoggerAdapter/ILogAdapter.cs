using System;
using System.Collections.Generic;
using LokiLogger.Shared;

namespace LokiLogger.LoggerAdapter {
	public interface ILogAdapter {
		void Write(string typ, LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects);
	}
}