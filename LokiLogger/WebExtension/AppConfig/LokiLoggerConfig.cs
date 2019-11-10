namespace LokiLogger.WebExtension.AppConfig {
	public class LokiLoggerConfig
	{
		public string Secret { get; set; }
		public string HostName { get; set; }
		public int SendInterval { get; set; } = 5;
	}

}