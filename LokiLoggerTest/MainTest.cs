using System;
using LokiLogger;
using LokiLogger.Model;
using Xunit;

namespace LokiLoggerTest {
	public class MainTest {
		[Fact]
		public void AddLogs()
		{
			Loki.Information("Hallo");
		}

		[Fact]
		public void TestIgnore()
		{
			try
			{
				Assert.True(true);
			}
			catch (Exception e)
			{
				Assert.False(true);
			}
		}
	}
}