using System;
using LokiWebExtension.Interception;
using LokiWebExtension.Interception.Interfaces;

namespace LokiWebExtension {
	public class LokiInterceptor :IMethodInterceptor{
		public void BeforeInvoke(InvocationContext invocationContext)
		{
			// Create a logger based on the class type that owns the executing method
			Type tmp = invocationContext.GetOwningType();
			LokiAttribute loggingAttribute = invocationContext.GetAttributeFromMethod<LokiAttribute>();
			

			// Get the Logging Level

			// Log the method being executed
			Console.WriteLine($"{invocationContext.GetOwningType()}: Method executing: {invocationContext.GetExecutingMethodName()}");
		}

		/// <inheritdoc />
		public void AfterInvoke(InvocationContext invocationContext, object methodResult)
		{
			Console.WriteLine($"{invocationContext.GetOwningType()}: Method executed: {invocationContext.GetExecutingMethodName()}");
			Console.WriteLine(methodResult);
		}
	}
}