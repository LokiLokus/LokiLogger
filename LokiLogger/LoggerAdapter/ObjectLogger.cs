using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Timers;
using LokiLogger.Model;
using Newtonsoft.Json;

namespace LokiLogger.LoggerAdapter
{
    public class ObjectLogger:ILogAdapter
    {
        private string _hostName;
        private string _name;
        private int _timeInterval;
        private ConcurrentQueue<Log> _logs;
        private Timer _timer;
        private HttpClient _client;
        
        public ObjectLogger(string hostName,string name,int timeInterval = 2000)
        {
            _logs = new ConcurrentQueue<Log>();
            _hostName = hostName;
            _timeInterval = timeInterval;
            _timer = new Timer(timeInterval);
            _timer.AutoReset = true;
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            List<Log> tmpSafe = new List<Log>();
            Log tmp;
            while (_logs.TryDequeue(out tmp))
            {
                tmpSafe.Add(tmp);
            }

            try
            {
                _client.PostAsJsonAsync(_hostName, tmpSafe);
            }
            catch (Exception exception)
            {
                tmpSafe.ForEach(x => _logs.Enqueue(x));
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
                Name = _name
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
                Name = _name
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
                Name = _name
            };
            _logs.Enqueue(result);
        }
    }

    public class Log
    {
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
}