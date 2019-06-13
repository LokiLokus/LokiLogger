using System;
using System.Diagnostics;
using System.Reflection;
using MethodDecorator.Fody.Interfaces;

namespace LokiLogger {
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Module,Inherited = false)]
	public class LokiAttribute :Attribute,IMethodDecorator{
		private readonly Stopwatch _stopwatch = new Stopwatch();
		public void Init(object instance, MethodBase method, object[] args)
		{
			throw new NotImplementedException();
		}

		public void OnEntry()
		{
			_stopwatch.Start();
			throw new NotImplementedException();
		}

		public void OnExit()
		{
			_stopwatch.Stop();
			throw new NotImplementedException();
		}

		public void OnException(Exception exception)
		{
			_stopwatch.Stop();
			throw new NotImplementedException();
		}
	}
}