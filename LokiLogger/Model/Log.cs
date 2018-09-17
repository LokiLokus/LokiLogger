using System;

namespace LokiLogger.Model {
	public class Log {
		public int ID { get; set; }
		public DateTime Time { get; set; }
		public LogType Type { get; set; }
		public string Message { get; set; }
		public string Class { get; set; }
		public string Method { get; set; }
		public int LineNr { get; set; }
	}
}