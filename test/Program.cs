using System;
using LokiLogger;
using LokiLogger.LoggerAdapter;

namespace test {
	class Program {
		[Loki]
		static void Main(string[] args)
		{
			Loki.UpdateAdapter(new BasicLoggerAdapter());
			Console.WriteLine("Hello World!");
			test();
		}
		
		[Loki]
		public static void test()
		{
			Console.WriteLine("Hallo");
		}
	}
}