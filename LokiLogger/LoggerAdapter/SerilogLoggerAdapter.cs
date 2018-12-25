
using LokiLogger.Model;

namespace LokiLogger.LoggerAdapter {
	public class SerilogLoggerAdapter : ILogAdapter {
		public void Write(LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects)
		{
			throw new System.NotImplementedException();
		}
	}
}