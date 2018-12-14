using System;

namespace LokiLogger.Model {
	public class Log {
		public int ID { get; set; }
		public DateTime Time { get; set; }
		public LogLevel Level { get; set; }
		public string Message { get; set; }
		public string Class { get; set; }
		public string Method { get; set; }
		public int LineNr { get; set; }

		public override string ToString()
		{
			return "{\"ID\":" + ID + ","
			       + "\"Time\":\"" + Time + "\","
			       + "\"Type\":" + (int) Level + ","
			       + "\"Message\":\"" + Message + "\","
			       + "\"Class\":\"" + Class + "\","
			       + "\"Method\":\"" + Method + "\","
			       + "\"LineNr\":" + LineNr
			       + "}";
		}
	}
}