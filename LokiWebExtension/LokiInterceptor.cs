using System;
using System.Collections.Generic;
using System.Linq;
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
			var parameters = invocationContext.GetExecutingMethodInfo().GetParameters().Select(x => x.Position);
			List<object> param = new List<object>();
			foreach (int i in parameters)
			{
				param.Add(invocationContext.GetParameterValue(i));
			}
			LogLevel logLevel;
			if (_attribute.ReturnLevel == null)
			{
				logLevel = _attribute.ParameterLevel.GetValueOrDefault();
			}
			else
			{
				logLevel = LokiObjectAdapter.LokiConfig.AttributeDefaultInvokeLevel;
			}
			Loki.WriteInvoke(logLevel,invocationContext.GetExecutingMethodName(),invocationContext.GetExecutingMethodInfo().DeclaringType.FullName,param);
		}

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