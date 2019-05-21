using LokiLogger.Model;
using LokiWebExtension.Interception.Attributes;

namespace LokiWebExtension {
	
	public class LokiAttribute : MethodInterceptionAttribute {
		public LogLevel? ParameterLevel;
		public LogLevel? ReturnLevel;
		public bool? StopTime;

		public LokiAttribute()
		{
		}

		public LokiAttribute(LogLevel parameterLevel)
		{
			ParameterLevel = parameterLevel;
		}
		public LokiAttribute(LogLevel parameterLevel,LogLevel returnLevel)
		{
			ParameterLevel = parameterLevel;
			ReturnLevel = returnLevel;
		}
		public LokiAttribute(LogLevel parameterLevel,LogLevel returnLevel,bool stopTime)
		{
			ParameterLevel = parameterLevel;
			ReturnLevel = returnLevel;
			StopTime = stopTime;
		}
	
		public LokiAttribute(bool stopTime)
		{
			StopTime = stopTime;
		}
	}
}