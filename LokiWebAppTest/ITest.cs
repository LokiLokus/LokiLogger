using System;

namespace LokiWebAppTest {
	public interface ITest {
		void Test();
		string Test1();
		void Test1(string test);
		string Test2(string test);
		string Test2(string test, int hallo);
	}
	public class Tester: ITest{
		public void Test()
		{
			Console.WriteLine("asdasd");
		}

		public string Test1()
		{
			return "Hallo" + "qasd";
		}

		public void Test1(string test)
		{
			Console.WriteLine(test);
		}

		public string Test2(string test)
		{
			Console.WriteLine(test);
			return test + Test1();
		}

		public string Test2(string test, int hallo)
		{
			Console.WriteLine(test + hallo);
			return test + hallo;
		}
	}
}