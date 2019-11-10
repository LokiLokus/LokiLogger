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
using LokiLogger.Shared;
using LokiWebExtension.ConfigSettings;
using LokiWebExtension.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace LokiWebExtension {
	public class LokiObjectAdapter : ILogAdapter,IDisposable,IHostedService{
        private ConcurrentQueue<Log> _logs;
        private HttpClient _client;
	    private System.Threading.Timer _timer;
	    
	    
	    public static LokiConfigSettings LokiConfig { get; set; }
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
                        var result = _client.PostAsJsonAsync(LokiConfig.HostName, tmpSafe);
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
                Name = LokiConfig.Name,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            };
            _logs.Enqueue(result);
        }

        public void WriteReturn(LogLevel logLevel, string message, string className, string methodName, int lineNr, object objects,long elapsedTime)
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
                LogTyp = Loki.Re,
                Message = message,
                Data = data,
                Name = LokiConfig.Name,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                ElapsedTime = elapsedTime
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
                exData = exception.Message + "\n";
                exData += exception.StackTrace;
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
                LogTyp = EXCEPTION_TYP,
                Message = message,
                Exception = exData,
                Data = data,
                Name = LokiConfig.Name,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            };
            _logs.Enqueue(result);
        }

        public void WriteInvoke(LogLevel logLevel, string methodName, string className, object[] objects)
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
                Method = methodName,
                LogLevel = logLevel,
                Class = className,
                Line = -1,
                LogTyp = LogTyp.Invoke,
                Message = "",
                Exception = null,
                Data = data,
                Name = LokiConfig.Name,
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
	            TimeSpan.FromSeconds(LokiConfig.SendInterval));
	        return Task.CompletedTask;
	    }

	    public Task StopAsync(CancellationToken cancellationToken)
	    {
	        
	        _timer?.Change(Timeout.Infinite, 0);
	        return Task.CompletedTask;
	    }
	}

    public static class LokiWebServiceExtension {
        
        public static IServiceCollection AddLokiObjectLogger(this IServiceCollection services, Action<LokiConfigSettings> options)
        {
            LokiConfigSettings config = new LokiConfigSettings();
            options.Invoke(config);
            LokiObjectAdapter.LokiConfig = config;
            services.AddHostedService<LokiObjectAdapter>();
            return services;
        }
    }

    
}