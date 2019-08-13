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

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                await LogResponse(context.Response,log);

                await responseBody.CopyToAsync(originalBodyStream);
            }

            LogLevel lvl = LokiObjectAdapter.LokiConfig.DefaultLevel;
            if (log.StatusCode >= 200 && log.StatusCode < 300)
            {
                lvl = LogLevel.Warning;

            }

            Loki.Write(LogTyp.RestCall, lvl, "", "Invoke", "LokiWebExtension.Middleware.LokiMiddleware", 55, log);
        }

        [Loki]
        private async Task<WebRestLog> LogRequest(HttpRequest request,WebRestLog log)
        {
            var body = request.Body;

            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            log.Scheme = request.Scheme;
            log.Host = request.Host.ToString();
            log.Path = request.Path;
            log.QueryString = request.QueryString.ToString();
            log.RequestBody = bodyAsText;
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
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}