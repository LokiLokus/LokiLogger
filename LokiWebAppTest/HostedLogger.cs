using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LokiWebAppTest {
	public class HostedLogger  : IHostedService, IDisposable{
		private Timer _timer;

		public HostedLogger()
		{
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{

			_timer = new Timer(DoWork, null, TimeSpan.Zero, 
				TimeSpan.FromSeconds(5));

			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			Console.WriteLine("I DO STUFF HERE");
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}