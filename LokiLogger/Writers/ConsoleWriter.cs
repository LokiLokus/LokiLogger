using System;
using System.Threading.Tasks;
using LokiLogger.Model;

namespace LokiLogger.Writers {
	public class ConsoleWriter : IWriter {
		public void WriteLog(Model.Log log)
		{
			switch (log.Type)
			{
				case LogType.Verbose:
					Console.ForegroundColor = ConsoleColor.Gray;
					break;
				case LogType.Debug:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case LogType.Information:
					Console.ForegroundColor = ConsoleColor.Black;
					break;
				case LogType.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case LogType.Critical:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case LogType.SystemCritical:
					Console.ForegroundColor = ConsoleColor.DarkRed;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			Console.BackgroundColor = ConsoleColor.White;
			Console.WriteLine("[{0}: {1}.{2}:{3}]{4}", log.Time.ToLongTimeString(), log.Class, log.Method, log.LineNr,
				log.Message);
		}

		public async Task Stop()
		{
			
		}
	}
}