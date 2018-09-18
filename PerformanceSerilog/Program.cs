using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace PerformanceSerilog {
	internal class Program {
		private static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.File("/home/lokilokus/Downloads/TestLog.json")
				.CreateLogger();

			DateTime tmpdate = DateTime.Now;
			int count = 20000;

			List<Task> tmp = new List<Task>();
			for (int i = 0; i < count; i++)
				tmp.Add(new Task(() =>
				{
					Log.Information("Hallo");
					Thread.Sleep(1);
					Log.Warning("HAsdas");
					Log.Error("asdas");
					Thread.Sleep(1);
					Log.Fatal("asdsad");
				}));

			tmp.ForEach(x => { x.Start(); });
			Task.WaitAll(tmp.ToArray());

			Console.WriteLine((DateTime.Now - tmpdate).TotalSeconds);

			Console.ReadKey();
		}
	}
}