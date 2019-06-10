using System;
using System.Dynamic;
using System.Linq.Expressions;
using KingAOP;
using LokiWebExtension;

namespace LokiWebAppTest {
	public interface ITest {
		void Test();
		string Test1();
		void Test1(string test);
		string Test2(string test);
		string Test2(string test, int hallo);
	}
	public class Tester: ITest ,IDynamicMetaObjectProvider{
		[HelloWorldAspect]
		public void Test()
		{
			Tester d = new Tester();
			d.Test1();
			
			Console.WriteLine("asdasd");
		}
		[Loki]
		public string Test1()
		{
			return "Hallo" + "qasd";
		}
		[HelloWorldAspect]
		public void Test1(string test)
		{
			Console.WriteLine(test);
		}
		[HelloWorldAspect]
		public string Test2(string test)
		{
			Console.WriteLine(test);
			return test + Test1();
		}
		[HelloWorldAspect]
		public string Test2(string test, int hallo)
		{
			Console.WriteLine(test + hallo);
			return test + hallo;
		}

		public DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new AspectWeaver(parameter, this);
		}
	}
}