using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger;

namespace PerformanceTest {
	internal class Program {
		private static void Main(string[] args)
		{
			int count = 2000;
			DateTime tmpdate = DateTime.Now;

			List<Task> tmp = new List<Task>();
			for (int i = 0; i < count; i++)
				tmp.Add(new Task(() =>
				{
					Log.Info("Hallo");
					Thread.Sleep(1);
					Log.Warn("HAsdas");
					Log.Crit("asdas");
					Thread.Sleep(1);
					Log.SysCrit("asdsad");
				}));

			tmp.ForEach(x => { x.Start(); });
			Task.WaitAll(tmp.ToArray());

			Console.WriteLine((DateTime.Now - tmpdate).TotalSeconds);
			Console.ReadKey();
		}
	}
}