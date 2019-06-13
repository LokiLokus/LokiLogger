using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LokiLogger;
using LokiLogger.LoggerAdapter;

namespace ConsoleApp1 {
	class Program {
		static void Main(string[] args)
		{
			/*Loki.UpdateAdapter(new BasicLoggerAdapter());
			Console.WriteLine("Hello World!");
			Console.WriteLine("Hi");
			var d = 3 + 1;
			

			string data = d + "Hi";
			Console.WriteLine(data);
			Console.WriteLine(FUCKYOU("Hallo",new List<int>(){1,2,3}));
			Test();
			Test2().Wait();
			Test3().Wait();
			Test4("§ln23", 4).Wait();*/
		}
		
		[Loki()]
		static void Test()
		{
			Console.WriteLine("Test");
			HttpClient client = new HttpClient();
			Stopwatch sw = new Stopwatch();
			sw.Start();
			client.GetAsync("asd").Wait();
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds);
		}
		
		 private static async Task<string> Test4(string ln23, int i)
		{
			await Task.Delay(123);
			return ln23 + i;
		}
/*
		[Loki("JoDigga")]
		static string FUCKYOU(string d,List<int> f)
		{
			Console.WriteLine("Hello World!");
			return "asdasd" + d + f.FirstOrDefault();
		}
		[Loki("asdas")]
		static async Task Test2()
		{
			Console.WriteLine("Test");
			await Task.Delay(2);
		}
		
		[Loki("asdas")]
		static async Task<string> Test3()
		{
			Console.WriteLine("Test");
			await Task.Delay(20);
			return "asddfsfdas";
		}*/
	}
}