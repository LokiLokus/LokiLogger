using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger;
using LokiLogger.Model;

namespace PerformanceTest {
	internal class Program {
		private static void Main(string[] args)
		{
			int count = 20;
			DateTime tmpdate = DateTime.Now;
			Loki.ProjectNameSpace = "lokilogger";

			Loki.Information("Hallo");
			Console.WriteLine((DateTime.Now - tmpdate).TotalSeconds);
			Console.ReadKey();
		}
	}
}