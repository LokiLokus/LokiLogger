using System;
using System.Diagnostics;
using System.Reflection;
using MethodDecorator.Fody.Interfaces;

namespace LokiLogger {
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Module,Inherited = false)]
	public class LokiAttribute :Attribute,IMethodDecorator{
		private readonly Stopwatch _stopwatch = new Stopwatch();
		private string _methodName;
		private string _className;
		public void Init(object instance, MethodBase method, object[] args)
		{
			_methodName = method.Name;
			_className = method.ReflectedType.FullName;
			
			Loki.WriteInvoke(_methodName,_className,args);
		}

		public void OnEntry()
		{
			_stopwatch.Start();
		}

		public void OnExit()
		{
			_stopwatch.Stop();
			Loki.WriteReturn(null,_methodName,_className,0,elapsedTime:_stopwatch.ElapsedTicks);
		}

		public void OnException(Exception exception)
		{
			_stopwatch.Stop();
			Loki.WriteException(exception,_methodName,_className,0);
		}
	}
}