using System;
using System.Collections.Generic;
using System.Linq;
using LokiLogger;
using LokiLogger.LoggerAdapter;
using LokiLogger.Model;
using Xunit;

namespace LokiLoggerTest {
	public class MainTest {
		[Fact]
		public void AddLogs()
		{
			Loki.UpdateAdapter(new SerilogLoggerAdapter());
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