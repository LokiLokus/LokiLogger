using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger.Model;
using LokiLogger.Writers;
using Log = LokiLogger.Log;

namespace PerformanceTest {
	internal class Program {
		private static void Main(string[] args)
		{
			int count = 20000;
			DateTime tmpdate = DateTime.Now;
			
			Log.SetWriter(new FileWriter("/home/lokilokus/Downloads/TestLog.json"));
			
			Log.IgnoreType(LogType.Debug);
			Log.IgnoreType(LogType.Verbose);
			Log.IgnoreType(LogType.Information);
			Log.IgnoreType(LogType.Warning);

			List<Task> tmp = new List<Task>();
			for (int i = 0; i < count; i++)
				tmp.Add(new Task(() =>
				{
					Log.Debug("asdsa");
					Log.Verbose("asdsa");
					Log.Info("Hallo");
					Thread.Sleep(1);
					Log.Warn("HAsdas");
					Log.Crit("asdas");
					Thread.Sleep(1);
					Log.SysCrit("asdsad");
				}));

			tmp.ForEach(x => { x.Start(); });
			Task.WaitAll(tmp.ToArray());
			Task l = Log.StopLog();
			l.Wait();

			Console.WriteLine((DateTime.Now - tmpdate).TotalSeconds);
			Console.ReadKey();
		}
	}
}