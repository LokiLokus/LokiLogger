using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger.LoggerAdapter;
using LokiLogger.Shared;
using LokiLogger.WebExtension.ConfigSettings;
using LokiLogger.WebExtension.Middleware;
using LokiWebExtension.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace LokiLogger.WebExtension {
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
				Data = data,
				Exception = exData
			};
			
			_logs.Enqueue(log);
		}
		
		
		public class SendLogModel
		{
			public string SourceSecret { get; set; }
			public List<Log> Logs { get; set; }
        
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
                SendLogModel sendData = new SendLogModel()
                {
	                Logs = tmpSafe,
	                SourceSecret = LokiConfig.Secret
                };
	            
                try
                {
                    if(tmpSafe.Count > 0){
                        var result = _client.PostAsJsonAsync(LokiConfig.HostName, sendData);
                        var data = result.Result;
                        if(!data.IsSuccessStatusCode)
                        {
	                        Console.WriteLine("Error on sending LokiLogger Data");
	                        Console.WriteLine("Status Code: " + data.StatusCode);
	                        Console.WriteLine("Message: " + data.Content.ReadAsStringAsync().Result);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Send Log Data Failed");
                    Console.WriteLine(exception.Message);
                    tmpSafe.ForEach(x => _logs.Enqueue(x));
                }
            }
        }

        public void Dispose()
	    {
		    try
		    {
			    _timer.Dispose();
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
		    }
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
}