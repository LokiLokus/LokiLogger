using System;
using LokiLogger.Model;
using Xunit;
using Log = LokiLogger.Log;

namespace LokiLoggerTest {
	public class MainTest {
		[Fact]
		public void AddLogs()
		{
		}

		[Fact]
		public void TestIgnore()
		{
			try
			{
				Log.DeIgnoreType(LogLevel.Debug);
				Assert.True(true);
			}
			catch (Exception e)
			{
				Assert.False(true);
			}
		}
	}
}