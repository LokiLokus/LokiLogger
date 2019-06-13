using System;
using System.Reflection;
using LokiLogger;
using LokiWebAppTest;
using MethodDecorator.Fody.Interfaces;
[module: Interceptor]
namespace LokiWebAppTest
{
    

// Any attribute which provides OnEntry/OnExit/OnException with proper args
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Module)]
    public class InterceptorAttribute : Attribute, IMethodDecorator	{
        // instance, method and args can be captured here and stored in attribute instance fields
        // for future usage in OnEntry/OnExit/OnException
        public void Init(object instance, MethodBase method, object[] args) {
            Loki.Information("Init: {args} ",method.DeclaringType.FullName + "." + method.Name,args);
        }

        public void OnEntry() {
            Loki.Information("OnEntry");
        }

        public void OnExit() {
            Loki.Information("OnExit");
        }

        public void OnException(Exception exception) {
            Loki.Information(string.Format("OnException: {0}: {1}", exception.GetType(), exception.Message));
        }
    }

}