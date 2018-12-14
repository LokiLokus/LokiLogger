using LokiLogger.Model;

namespace LokiLogger.LoggerAdapter {
	public interface ILogAdapter {
		void Write(LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects);
	}
}