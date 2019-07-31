using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LokiLogger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace LokiWebExtension.Middleware {
	public class LokiMiddleware {
		private readonly RequestDelegate _next;

        public LokiMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            RequestLog log;
            try
            {
                log = await LogRequest(context.Request,context.TraceIdentifier);
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

                string response = await LogResponse(context.Response,log);


                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        [Loki]
        private async Task<RequestLog> LogRequest(HttpRequest request,string traceId)
        {
            var body = request.Body;

            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            var data = new RequestLog{
                Scheme = request.Scheme,
                Host = request.Host.ToString(),
                Path = request.Path,
                QueryString = request.QueryString.ToString(),
                Body = bodyAsText,
                ClientIp = request.HttpContext.Connection.RemoteIpAddress.ToString(),
                TraceId = traceId
            };
            
            Loki.WriteInvoke("LogRequest","LokiWebExtension.Middleware.LokiMiddleware", data);
            return data;
        }

        [Loki]
        private async Task LogResponse(HttpResponse response,RequestLog log)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            ResponseLog data; 
            if (log != null)
            {
                data = new ResponseLog{
                    Scheme = log.Scheme,
                    Host = log.Host,
                    Path = log.Path,
                    QueryString = log.QueryString,
                    Body = text,
                    ClientIp = log.ClientIp,
                    TraceId = log.TraceId,
                    StatusCode = response.StatusCode,
                };
            }
            else
            {
                data = new ResponseLog{
                    Body = text,
                    ClientIp = response.HttpContext.Connection.RemoteIpAddress.ToString(),
                    TraceId = response.HttpContext.TraceIdentifier,
                    StatusCode = response.StatusCode,
                };
            }
            Loki.WriteReturn(data,"LogResponse","LokiWebExtension.Middleware.LokiMiddleware",89);
        }
	}

    class HttpContextLog {
        
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string ClientIp { get; set; }
        public string TraceId { get; set; }
    }
    class RequestLog :HttpContextLog {
        public string Body { get; set; }
    }

    class ResponseLog : HttpContextLog {
        public string Body { get; set; }
        public int StatusCode { get; set; }
        
    }
}