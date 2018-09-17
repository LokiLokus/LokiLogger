using LokiLogger;
using Xunit;

namespace LokiLoggerTest {
	public class MainTest {
		[Fact]
		public void AddLogs()
		{
			Log.Info("Hallo");
			Log.Warn("Hallo");
			Log.Crit("Hallo");
			Log.SysCrit("Hallo");
		}
	}
}