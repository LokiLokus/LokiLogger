using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            
            Stopwatch overall = Stopwatch.StartNew();
            if (!LokiObjectAdapter.LokiConfig.UseMiddleware || LokiObjectAdapter.LokiConfig.IgnoreRoutes.Any(x => context.Request.Path.ToString().Contains(x))) await _next(context);
            else
            {
                Stopwatch test1 = Stopwatch.StartNew();
                WebRestLog log = new WebRestLog()
                {
                    TraceId = context.TraceIdentifier
                };
                try
                {
                    
                    log = await LogRequest(context.Request,log);
                    test1.Stop();
                    Console.WriteLine("TEST1 \t " + test1.ElapsedMilliseconds);
                }
                catch (Exception e)
                {
                    Loki.ExceptionWarning("Error in Loki Middleware logging Request",e);
                }

                var originalBodyStream = context.Response.Body;

                //Create a new memory stream...
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    try
                    {
                        test1 = Stopwatch.StartNew();
                        await _next(context);
                        
                        test1.Stop();
                        Console.WriteLine("TEST2 \t " + test1.ElapsedMilliseconds);
                        if (context.Items.ContainsKey("Exception"))
                        {
                            Exception ex = (Exception) context.Items["Exception"];
                            log.Exception = ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source;
                        }
                    }
                    catch (Exception e)
                    {
                        log.Exception = e.Message + "\n" + e.StackTrace + "\n" + e.Source;
                        await LogResponse(context.Response, log);
                        Loki.Write(LogTyp.RestCall, LogLevel.Error, "", "Invoke", "LokiWebExtension.Middleware.LokiMiddleware", 48, log);
                        throw;
                    }

                    try
                    {
                        test1 = Stopwatch.StartNew();
                        await LogResponse(context.Response, log);
                        
                        test1.Stop();
                        Console.WriteLine("TEST3 \t " + test1.ElapsedMilliseconds);
                    }
                    catch (Exception e)
                    {
                        Loki.ExceptionWarning("Error in Loki Middleware logging Response",e);
                    }
                    if(originalBodyStream.CanWrite && responseBody.CanRead)
                        await responseBody.CopyToAsync(originalBodyStream);
                }

                LogLevel lvl = LokiObjectAdapter.LokiConfig.DefaultLevel;
                if (!(200 <= log.StatusCode || log.StatusCode < 300))
                {
                    lvl = LogLevel.Warning;
                }

                Loki.Write(LogTyp.RestCall, lvl, "", "Invoke", "LokiWebExtension.Middleware.LokiMiddleware", 55, log);
            }
            overall.Stop();
            Console.WriteLine("overall \t " + overall.ElapsedMilliseconds);
            
        }

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

        private async Task LogResponse(HttpResponse response,WebRestLog log)
        {
            try
            {

                response.Body.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                string text = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                
                log.ResponseBody = text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            log.StatusCode = response.StatusCode;
            log.End = DateTime.UtcNow;
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