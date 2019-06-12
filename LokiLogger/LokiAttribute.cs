using System;

namespace LokiLogger {
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public class LokiAttribute :Attribute{
		public string Dasda { get; set; }

		public LokiAttribute(string d)
		{
			Dasda = d;
		}
	}
}