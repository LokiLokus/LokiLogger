using System;
using Microsoft.Extensions.Configuration;

namespace LokiLogger.WebExtension {
	public static class Config {
		
		private static T GetSettingRequired<T>(IConfiguration config, string section)
		{
			T setting = config.GetSection(section).Get<T>();
			if (setting == null) throw new NullReferenceException(section + " is null");
			return setting;
		}
		private static T GetSetting<T>(IConfiguration config, string section)
		{
			T setting = config.GetSection(section).Get<T>();
			return setting;
		}
	}
}