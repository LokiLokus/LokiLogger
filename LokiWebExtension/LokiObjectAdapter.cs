using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using LokiLogger;
using LokiLogger.LoggerAdapter;
using LokiLogger.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace LokiWebExtension {
	public class LokiObjectAdapter : ILogAdapter,IDisposable,IHostedService{
        private ConcurrentQueue<Log> _logs;
        private HttpClient _client;
	    private System.Threading.Timer _timer;
	    
	    
	    public static LokiConfig LokiConfig { get; set; }
	    public static string HostName { get; set; }
	    public static string Name { get; set; }
	    private static object _lock = new object();
        public LokiObjectAdapter()
        {
            _logs = new ConcurrentQueue<Log>();
            _client = new HttpClient();
            Loki.UpdateAdapter(this);
        }



	    [MethodImpl(MethodImplOptions.Synchronized)]
        private void SendData(object state)
        {
            lock(_lock){
                List<Log> tmpSafe = new List<Log>();
                Log tmp;
                while (_logs.TryDequeue(out tmp))
                {
                    tmpSafe.Add(tmp);
                }
    
                try
                {
                    if(tmpSafe.Count > 0){
                        var result = _client.PostAsJsonAsync(HostName, tmpSafe);
                        var data = result.Result;
                        if(!data.IsSuccessStatusCode) throw new Exception();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error occured");
                    tmpSafe.ForEach(x => _logs.Enqueue(x));
                }
            }
        }
        
        public void Write(LogLevel logLevel, string message, string className, string methodName, int line, params object[] objects)
        {
            string data;
            try
            {
                
                if(objects == null) 
                    data = null;
                else
                    data = JsonConvert.SerializeObject(objects);
            }
            catch (Exception e)
            {
                data = "Error on Serialize";
            }
            Log result = new Log()
            {
                Time = DateTime.Now,
                LogLevel = logLevel,
                Method = methodName,
                Class = className,
                Line = line,
                LogTyp = LogTyp.Normal,
                Message = message,
                Data = data,
                Name = Name,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            };
            _logs.Enqueue(result);
        }

        public void WriteReturn(LogLevel logLevel, string message, string className, string methodName, int lineNr, object objects)
        {
            string data;
            try
            {
                if(objects == null) 
                    data = null;
                else
                    data = JsonConvert.SerializeObject(objects);
            }
            catch (Exception e)
            {
                data = "Error on Serialize";
            }
            Log result = new Log()
            {
                Time = DateTime.Now,
                LogLevel = logLevel,
                Method = methodName,
                Class = className,
                Line = lineNr,
                LogTyp = LogTyp.Return,
                Message = message,
                Data = data,
                Name = Name,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            };
            _logs.Enqueue(result);
        }

        public void WriteException(LogLevel logLevel, string message, string className, string methodName, int lineNr,
            Exception exception, params object[] objects)
        {
            string data;
            string exData;
            try
            {
                exData = JsonConvert.SerializeObject(exception);
                if(objects == null) 
                    data = null;
                else
                    data = JsonConvert.SerializeObject(objects);
            }
            catch (Exception e)
            {
                data = "Error on Serialize";
                exData = "Error on Serialize";
            }
            Log result = new Log()
            {
                Time = DateTime.Now,
                LogLevel = logLevel,
                Method = methodName,
                Class = className,
                Line = lineNr,
                LogTyp = LogTyp.Exception,
                Message = message,
                Exception = exData,
                Data = data,
                Name = Name,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            };
            _logs.Enqueue(result);
        }

	    public void Dispose()
	    {
	        _timer.Dispose();
	        SendData(null);
	        _client.Dispose();
	    }

	    public Task StartAsync(CancellationToken cancellationToken)
	    {
	        
	        _timer = new System.Threading.Timer(SendData, null, TimeSpan.Zero, 
	            TimeSpan.FromSeconds(5));

	        return Task.CompletedTask;
	    }

	    public Task StopAsync(CancellationToken cancellationToken)
	    {
	        
	        _timer?.Change(Timeout.Infinite, 0);

	        return Task.CompletedTask;
	    }
	}

    public class Log
    {
        public int ThreadId { get; set; }
        public DateTime Time { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public int Line { get; set; }
        public LogTyp LogTyp { get; set; }
        public string Exception { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
    }

    public enum LogTyp
    {
        Normal,Exception,Return
    }

    public static class LokiWebServiceExtension {
        
        public static IServiceCollection AddLokiObjectLogger(this IServiceCollection services, Action<LokiConfig> options)
        {
            LokiConfig config = new LokiConfig();
            options.Invoke(config);
            LokiObjectAdapter.LokiConfig = config;
            services.AddHostedService<LokiObjectAdapter>();
            return services;
        }
    }

    public class LokiConfig {
        public string HostName { get; set; }
        public string Name { get; set; }
        public bool ActivateAttributes { get; set; }
        public bool EnableStopWatch { get; set; }
        public LogLevel AttributeDefaultInvokeLevel { get; set; }
        public LogLevel AttributeDefaultEndLevel { get; set; }
    }
}