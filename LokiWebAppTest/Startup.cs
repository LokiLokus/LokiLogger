using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokiLogger;
using LokiWebExtension;
using LokiWebExtension.Interception.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LogLevel = LokiLogger.Model.LogLevel;

namespace LokiWebAppTest {
	public class Startup {
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			Loki.Information("Hallo");
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			
			services.AddLokiObjectLogger(options =>
			{
				options.Name = "TEST";
				options.HostName = "https://llogger.hopfenspace.org/api/Logging/Log";
				options.ActivateAttributes = false;
				options.AttributeDefaultInvokeLevel = LogLevel.Information;
				options.AttributeDefaultEndLevel = LogLevel.Information;
				options.SendInterval = 1;

			});
			
			services.AddTransient<ITest, Tester>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}