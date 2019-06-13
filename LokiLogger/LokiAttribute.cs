using System;
using System.Diagnostics;
using System.Reflection;

namespace LokiLogger {
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Module,Inherited = false)]
	public class LokiAttribute :Attribute{
	}
}