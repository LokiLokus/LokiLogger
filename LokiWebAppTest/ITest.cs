using System;
using LokiWebExtension;

namespace LokiWebAppTest {
	public interface ITest {
		void Test();
		string Test1();
		void Test1(string test);
		string Test2(string test);
		string Test2(string test, int hallo);
	}
	public class Tester: ITest{
		[Loki]
		public void Test()
		{
			Console.WriteLine("asdasd");
		}
		[Loki]
		public string Test1()
		{
			return "Hallo" + "qasd";
		}
		[Loki]
		public void Test1(string test)
		{
			Console.WriteLine(test);
		}
		[Loki]
		public string Test2(string test)
		{
			Console.WriteLine(test);
			return test + Test1();
		}
		[Loki]
		public string Test2(string test, int hallo)
		{
			Console.WriteLine(test + hallo);
			return test + hallo;
		}
	}
}