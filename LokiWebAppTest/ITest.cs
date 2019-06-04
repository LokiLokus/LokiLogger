using System;
using LokiWebExtension;

namespace LokiWebAppTest {
	public interface ITest {
		[Loki]
		void Test();
		string Test1();
		void Test1(string test);
		string Test2(string test);
		[Interceptor]
		string Test2(string test, int hallo);
	}
	public class Tester: ITest{
		
		[Interceptor]
		public void Test()
		{
			Console.WriteLine("asdasd");
		}
		[Interceptor]
		public string Test1()
		{
			return "Hallo" + "qasd";
		}
		[Loki]
		public void Test1(string test)
		{
			Console.WriteLine(test);
		}
		[Interceptor]
		public string Test2(string test)
		{
			Console.WriteLine(test);
			return test + Test1();
		}
		[Interceptor]
		public string Test2(string test, int hallo)
		{
			Console.WriteLine(test + hallo);
			return test + hallo;
		}
	}
}