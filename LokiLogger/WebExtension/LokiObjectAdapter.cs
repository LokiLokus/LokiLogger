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
		
		public void Write(LogTyp typ, LogLevel logLevel, string message, string className, string methodName, int line,
			params object[] objects)
		{
			string data = "";
			string exData = "";
			
			if (objects.Length > 0)
			{
				if (typ == LogTyp.Exception)
				{
					try
					{
						Exception exception = (Exception) objects[0];
						exData = exception.Message + "\n";
						exData += exception.StackTrace;
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}
				try
				{
					data = JsonConvert.SerializeObject(objects);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			
			Log log = new Log()
			{
				Method = methodName,
				Class = className,
				Line = line,
				Message = message,
				Time = DateTime.UtcNow,
				ThreadId = Thread.CurrentThread.ManagedThreadId,
				LogLevel = logLevel,
				LogTyp = typ,
				Name = LokiConfig.Name,
				Data = data,
				Exception = exData
			};
			
			_logs.Enqueue(log);
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