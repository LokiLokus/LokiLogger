using System.Collections.Generic;
using LokiLogger.Shared;

namespace LokiLogger.WebExtension.ConfigSettings {
	public class LokiConfigSettings {
		public string Secret { get; set; }
		public string HostName { get; set; }
		public bool UseMiddleware { get; set; } = true;
		public bool DefaultLokiControllerRethrowException { get; set; } = false;
		public string DefaultControllerErrorMessage { get; set; } = "Error occured";

		public int SendInterval { get; set; } = 5;
		public LogLevel DefaultLevel { get; set; }
		public List<string> IgnoreRoutes { get; set; } = new List<string>();
		public string DefaultControllerErrorCode { get; set; } = "General";
	}
}