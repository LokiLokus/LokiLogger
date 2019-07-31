using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using LokiLogger;
using LokiLogger.Shared;
using MethodDecorator.Fody.Interfaces;

	public class LokiAttribute:Attribute,IMethodDecorator {
		private Stopwatch _stopwatch;
		private string _methodName;
		private string _className;
		private readonly bool _enableStopwatch = true;
		
		public LokiAttribute(){}

		public LokiAttribute(bool enableStopWatch)
		{
			_enableStopwatch = enableStopWatch;
		}
		
		
		public void Init(object instance, MethodBase method, object[] args)
		{
			if(_enableStopwatch)
				_stopwatch = new Stopwatch();
			_methodName = method.Name;
			_className = method.DeclaringType.FullName;
			Loki.Write(LogTyp.Invoke,LogLevel.Debug,null,_methodName,_className,-1,args);
		}

		public void OnEntry()
		{
			if(_enableStopwatch)
				_stopwatch.Start();
		}

		public void OnExit()
		{
			if(_enableStopwatch){
				_stopwatch.Stop();
				Loki.Write(LogTyp.Return,LogLevel.Debug,null,_methodName,_className,-1,_stopwatch.ElapsedTicks);
			}
			else
			{
				Loki.Write(LogTyp.Return,LogLevel.Debug,null,_methodName,_className,-1);
			}
		}

		public void OnException(Exception exception)
		{
			if(_enableStopwatch)
				_stopwatch.Stop();
			Loki.Write(LogTyp.Exception,LogLevel.Warning,null,_methodName,_className,-1,exception);
		}
	}
