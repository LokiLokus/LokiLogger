using Microsoft.AspNetCore.Mvc;

namespace LokiLoggerReporter.Controllers {
	[Route("/")]
	public class HomeController :Controller{

		[HttpGet()]
		public IActionResult Index()
		{
			return View();
		}
	}
}