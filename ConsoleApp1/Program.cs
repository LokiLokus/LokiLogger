using System;
using System.Collections.Generic;
using System.Linq;
using LokiLogger;

namespace ConsoleApp1 {
	class Program {
		[Loki("JoDigga")]
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Console.WriteLine("Hi");
			var d = 3 + 1;

			string data = d + "Hi";
			Console.WriteLine(data);
			Console.WriteLine(FUCKYOU("Hallo",new List<int>(){1,2,3}));
		}
		
		[Loki("JoDigga")]
		static string FUCKYOU(string d,List<int> f)
		{
			Console.WriteLine("Hello World!");
			return "asdasd" + d + f.FirstOrDefault();
		}
	}
}