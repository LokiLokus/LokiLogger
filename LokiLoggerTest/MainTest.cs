using System;
using LokiLogger.Model;
using Xunit;
using Log = LokiLogger.Log;

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

		[Fact]
		public void TestIgnore()
		{
			try
			{
				Log.DeIgnoreType(LogType.Debug);
				Assert.True(true);
			}
			catch (Exception e)
			{
				Assert.False(true);
			}
		}
	}
}