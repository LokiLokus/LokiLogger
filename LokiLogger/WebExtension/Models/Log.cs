using System;
using LokiLogger.Shared;

namespace LokiWebExtension.Models {
	public class Log
	{
		public int ThreadId { get; set; }
		public DateTime Time { get; set; }
		public LogLevel LogLevel { get; set; }
		public string Message { get; set; }
		public string Class { get; set; }
		public string Method { get; set; }
		public int Line { get; set; }
		public LogTyp LogTyp { get; set; }
		public string Exception { get; set; }
		public string Data { get; set; }
		public string Name { get; set; }
	}
}