using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LokiLogger.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace LokiLogger.WebExtension.Middleware {
	public class LokiMiddleware {
		private readonly RequestDelegate _next;

        public LokiMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            WebRestLog log = new WebRestLog()
            {
                TraceId = context.TraceIdentifier
            };
            try
            {
                log = await LogRequest(context.Request,log);
            }
            catch (Exception e)
            {
                Loki.ExceptionWarning("Error in Loki Middleware logging Request",e);
            }



            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                log.Exception = e.Message + "\n" + e.StackTrace + "\n" + e.Source;
                await LogResponse(context.Response, log);
                Loki.Write(LogTyp.RestCall, LogLevel.Error, "", "Invoke", "LokiWebExtension.Middleware.LokiMiddleware", 48, log);
                throw;
            }
                
            await LogResponse(context.Response, log);
                


            LogLevel lvl = LokiObjectAdapter.LokiConfig.DefaultLevel;
            if (!(log.StatusCode >= 200 && log.StatusCode < 300))
            {
                lvl = LogLevel.Warning;

            }

            Loki.Write(LogTyp.RestCall, lvl, "", "Invoke", "LokiWebExtension.Middleware.LokiMiddleware", 55, log);
        }

        [Loki]
        private async Task<WebRestLog> LogRequest(HttpRequest request,WebRestLog log)
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, false, 1024, true))
            {
                log.RequestBody = await reader.ReadToEndAsync();

                request.Body.Seek(0, SeekOrigin.Begin);
            }
            
            log.Scheme = request.Scheme;
            log.Host = request.Host.ToString();
            log.Path = request.Path;
            log.QueryString = request.QueryString.ToString();
            log.ClientIp = request.HttpContext.Connection.RemoteIpAddress.ToString();
            log.HttpMethod = request.Method;
            log.Start = DateTime.UtcNow;
            
            return log;
        }

        [Loki]
        private async Task LogResponse(HttpResponse response,WebRestLog log)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            
            response.Body.Seek(0, SeekOrigin.Begin);

            log.StatusCode = response.StatusCode;
            log.End = DateTime.UtcNow;
            log.ResponseBody = text;
        }
	}

    class WebRestLog {
        public string HttpMethod { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string ClientIp { get; set; }
        public string TraceId { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public int StatusCode { get; set; }
        public string Exception { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}