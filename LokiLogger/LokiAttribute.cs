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
		
		
		public void Init(object instance, MethodBase method, object[] args)
		{
			_stopwatch = new Stopwatch();
			_methodName = method.Name;
			_className = method.DeclaringType.FullName;
			Loki.Write(Loki.INVOKE_TYP,LogLevel.SystemGenerated,null,_methodName,_className,-1,args);
		}

		public void OnEntry()
		{
			_stopwatch.Start();
		}

		public void OnExit()
		{
			_stopwatch.Stop();
			Loki.Write(Loki.RETURN_TYP,LogLevel.SystemGenerated,null,_methodName,_className,-1,_stopwatch.ElapsedTicks);
		}

		public void OnException(Exception exception)
		{
			_stopwatch.Stop();
			Loki.Write(Loki.EXCEPTION_TYP,LogLevel.SystemGenerated,null,_methodName,_className,-1,exception);
		}
	}
