using System;
using System.Collections.Generic;
using LokiLogger.Shared;
using LokiLogger.WebExtension.ConfigSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LokiLogger.WebExtension.Middleware {
	public static class LokiMiddlewareExtension {
		public static IApplicationBuilder UseLokiLogger(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<LokiMiddleware>();
			return builder;
		}

		public static IServiceCollection AddLokiObjectLogger(this IServiceCollection services,Action<LokiConfigSettings> configAction)
		{
			LokiConfigSettings config = new LokiConfigSettings();
			configAction.Invoke(config);
			return services.AddLokiObjectLogger(config);
		}
		public static IServiceCollection AddLokiObjectLogger(this IServiceCollection services,LokiConfigSettings config)
		{
			LokiObjectAdapter.LokiConfig = config;
			services.AddHostedService<LokiObjectAdapter>();
			return services;
		}
	}
}