using LokiLogger.Shared;

namespace LokiWebExtension.ConfigSettings {
	public class LokiConfigSettings {
			public string HostName { get; set; }
			public string Name { get; set; }
			public bool ActivateAttributes { get; set; }
			public LogLevel AttributeDefaultInvokeLevel { get; set; }
			public LogLevel AttributeDefaultEndLevel { get; set; }
			public int SendInterval { get; set; }
	}
}