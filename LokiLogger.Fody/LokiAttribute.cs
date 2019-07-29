using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using MethodDecorator.Fody.Interfaces;

namespace LokiLogger.Fody {
	public class LokiAttribute:Attribute,IMethodDecorator {
		private Stopwatch _stopwatch;
		private string _methodName;
		private string _className;
		private readonly bool _enableStopwatch;
		
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
			Loki.WriteInvoke(_methodName,_className,args);
			
		}

		public void OnEntry()
		{
			if(_enableStopwatch)
				_stopwatch.Start();
		}

		public void OnExit()
		{
			if(_enableStopwatch)
				_stopwatch.Stop();
			Loki.WriteReturn(null, _methodName, _className, -1, elapsedTime: _stopwatch.ElapsedTicks);
		}

		public void OnException(Exception exception)
		{
			if(_enableStopwatch)
				_stopwatch.Stop();
			Loki.WriteException(exception,_methodName,_className,-1);
		}
	}
}