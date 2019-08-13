using System;
using System.Collections.Generic;
using LokiLogger.Shared;
using LokiLogger.WebExtension.ConfigSettings;
using Microsoft.AspNetCore.Builder;

namespace LokiLogger.WebExtension.Middleware {
	public static class LokiMiddlewareExtension {
		internal static LokiConfigSettings Config { get; set; }

		public static IApplicationBuilder UseLokiLogger(this IApplicationBuilder builder,Action<LokiConfigSettings> configAction)
		{
			LokiConfigSettings config = new LokiConfigSettings();
			configAction.Invoke(config);
			return UseLokiLogger(builder,config);
		}
				
		public static IApplicationBuilder UseLokiLogger(this IApplicationBuilder builder)
		{
			LokiConfigSettings config = new LokiConfigSettings();
			return UseLokiLogger(builder,config);
		}
		
		private static IApplicationBuilder UseLokiLogger(IApplicationBuilder builder,LokiConfigSettings config)
		{
			Config = config;
			builder.UseMiddleware<LokiConfigSettings>();
			return builder;
		}
	}
}