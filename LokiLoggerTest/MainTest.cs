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
			Loki.UpdateAdapter(new BasicLoggerAdapter());
			Loki.Information("Hallo");
		}

		[Fact]
		public void TestIgnore()
		{
			Loki.ReturnInfo("Test",new List<string>(){"asd","awd"});
			try
			{
				throw new Exception("Test");
			}
			catch (Exception e)
			{
				Loki.ExceptionInformation(e);
			}
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