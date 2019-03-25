using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger;
using Serilog;

namespace PerformanceSerilog {
	internal class Program {
		private static void Main(string[] args)
		{
			DateTime tmpdate = DateTime.Now;


			for (int i = 0; i < 100000; i++)
			{
				Loki.Information("Hallo das ist das ding hier {s}",new Dictionary<string,string>(){{"Hallo","Hallo"}},"asdasd");
			}
			Console.WriteLine((DateTime.Now - tmpdate).TotalSeconds);
			Console.ReadKey();
		}
		
		public class Dummy {
			public int ID { get; set; }
		}
	}
}