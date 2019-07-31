using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LokiLogger.Shared;
using Microsoft.AspNetCore.Builder;

namespace LokiWebExtension.Middleware {
	public static class LokiMiddlewareExtension {
		internal static LokiMiddlewareConfig Config { get; set; }

		public static IApplicationBuilder UseLokiLogger(this IApplicationBuilder builder,Action<LokiMiddlewareConfig> configAction)
		{
			LokiMiddlewareConfig config = new LokiMiddlewareConfig();
			configAction.Invoke(config);
			return UseLokiLogger(builder,config);
		}
		
		
		public static IApplicationBuilder UseLokiLogger(this IApplicationBuilder builder)
		{
			LokiMiddlewareConfig config = new LokiMiddlewareConfig();
			return UseLokiLogger(builder,config);
		}
		
		private static IApplicationBuilder UseLokiLogger(IApplicationBuilder builder,LokiMiddlewareConfig config)
		{
			Config = config;
			builder.UseMiddleware<LokiMiddleware>();
			return builder;
		}
		
	}

	public class LokiMiddlewareConfig {
		public LogLevel DefaultLevel { get; set; }
		internal List<string> IgnoreRoutes { get; set; }
		internal Dictionary<string,LogLevel> Routes { get; set; }

		public LokiMiddlewareConfig()
		{
			DefaultLevel = LogLevel.Debug;
			IgnoreRoutes = new List<string>();
			Routes = new Dictionary<string, LogLevel>();
		}

		public void AddIgnoreRoutes(string route)
		{
			if(route ==null) throw new ArgumentNullException(nameof(route));
			IgnoreRoutes.Add(route);
		}
[Obsolete("This feature is not supported at the Moment")]
		public void SetLevelOfRoute(string route, LogLevel level)
		{
			if(route ==null) throw new ArgumentNullException(nameof(route));
			
			if(IgnoreRoutes.Contains(route)) throw new Exception("Route is already ignored");
			if(Routes.ContainsKey(route)) throw new Exception("LogLevel of Route already defined");
			Routes.Add(route,level);
		}
	}
}