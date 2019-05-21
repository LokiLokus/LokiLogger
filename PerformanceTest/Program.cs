using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using LokiLogger;
using LokiLogger.LoggerAdapter;
using AssemblyDefinition = Mono.Cecil.AssemblyDefinition;

namespace PerformanceTest {
	class Program {
		static void Main(string[] args)
		{
			
			
		}


		static void PerformanceTest()
		{
			
			Stopwatch swatch = new Stopwatch();
			bool attribute = true;
			swatch.Reset();
			swatch.Start();
			Loki.UpdateAdapter(new BasicLoggerAdapter());
			Dictionary<string,string> tmp = new Dictionary<string, string>();
			tmp.Add("Adsasdsa","adasdads");
			tmp.Add("Adassasdsa","adasdads");
			tmp.Add("Adsaassdsa","adasdads");
			tmp.Add("Adsassfddsa","adasdads");
			tmp.Add("Adsasdsgda","adasdads");
			for (int i = 0; i < 100000; i++)
			{
				if(attribute)
					MethodAttr("asdasd", 10,tmp);
				else
				{
					Method("asdasd", 10,tmp);
				}
			}
			swatch.Stop();
			Console.WriteLine("Time: " + swatch.ElapsedMilliseconds);
			Console.ReadLine();
		}
		
		[Loki]
		static string MethodAttr(string data,int a, Dictionary<string,string> dic)
		{
			Console.WriteLine("Hallo");
			data += "asdsad";
			data += a;
			foreach (var keyValuePair in dic)
			{
				data += keyValuePair.Key + keyValuePair.Value;
			}
			return data;
		}
		
		static string Method(string data,int a, Dictionary<string,string> dic)
		{
			Loki.Information("Init",data,a,dic);
			Console.WriteLine("Hallo");
			data += "asdsad";
			data += a;
			foreach (var keyValuePair in dic)
			{
				data += keyValuePair.Key + keyValuePair.Value;
			}
			Loki.ReturnInfo(data);
			return data;
		}
		static string MethodPlain(string data,int a, Dictionary<string,string> dic)
		{
			Console.WriteLine("Hallo");
			data += "asdsad";
			data += a;
			foreach (var keyValuePair in dic)
			{
				data += keyValuePair.Key + keyValuePair.Value;
			}
			return data;
		}
	}
}