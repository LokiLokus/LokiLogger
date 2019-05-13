using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger;
using LokiLoggerReporter.Config;
using LokiLoggerReporter.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LokiLoggerReporter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            DatabaseSettings databaseSettings = GetSettings<DatabaseSettings>("DatabaseSettings");

            services.AddDbContext<DatabaseContext>(opt => opt.UseMySql(databaseSettings.ConnectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private string ContentRootPath()
        {
            string fullPath = Assembly.GetExecutingAssembly().Location;
            int pos = fullPath.IndexOf("LokiLoggerReporter", StringComparison.Ordinal);
            string sharedFolder = fullPath.Substring(0, pos);

            sharedFolder += "LokiLogger";
            
            return sharedFolder;
        }
        
        private T GetSettings<T>(string section)
        {
            T setting = Configuration.GetSection(section).Get<T>();
            if (setting == null) throw new NullReferenceException(section + " is null");
            return setting;
        }
    }
}