using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger.Model;
using Log = LokiLogger.Log;

namespace PerformanceTest {
	internal class Program {
		private static void Main(string[] args)
		{
			int count = 20;
			DateTime tmpdate = DateTime.Now;


			Console.WriteLine((DateTime.Now - tmpdate).TotalSeconds);
			Console.ReadKey();
		}
	}
}