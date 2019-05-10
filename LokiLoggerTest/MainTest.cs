using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LokiLogger;
using LokiLogger.LoggerAdapter;
using LokiLogger.Model;
using Xunit;

namespace LokiLoggerTest {
	public class MainTest {
		[Fact]
		public void AddLogs()
		{
			Loki.UpdateAdapter(new ObjectLogger("http://localhost:5000/api/Logging/Log","Log",1000));
			for (int i = 0; i < 100; i++)
			{
				
				Loki.Information("Hallo");
				Loki.Information("Hallo");
				Loki.ReturnCritical("Hallo");
				Thread.Sleep(100);
			}
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