using System;
using LokiLogger;
using LokiLogger.Model;
using LokiWebExtension.Interception;
using LokiWebExtension.Interception.Interfaces;

namespace LokiWebExtension {
	public class LokiInterceptor :IMethodInterceptor {
		private LokiAttribute _attribute;
		public void BeforeInvoke(InvocationContext invocationContext)
		{
			// Create a logger based on the class type that owns the executing method
			Type tmp = invocationContext.GetOwningType();
			_attribute = invocationContext.GetAttributeFromMethod<LokiAttribute>();
			

			// Get the Logging Level

			// Log the method being executed
			Console.WriteLine($"{invocationContext.GetOwningType()}: Method executing: {invocationContext.GetExecutingMethodName()}");
		}

		/// <inheritdoc />
		public void AfterInvoke(InvocationContext invocationContext, object methodResult)
		{
			LogLevel tmp;
			if (_attribute.ReturnLevel == null)
			{
				tmp = _attribute.ReturnLevel.GetValueOrDefault();
			}
			else
			{
				tmp = LokiObjectAdapter.LokiConfig.AttributeDefaultEndLevel;
			}
			Loki.WriteReturn(tmp,methodResult,invocationContext.GetExecutingMethodName(),invocationContext.GetExecutingMethodInfo().DeclaringType.FullName,-1);
		}
	}
}