using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LokiLogger;
using Microsoft.AspNetCore.Mvc;

namespace LokiWebAppTest.Controllers {
	[Route("/")]
	[ApiController]
	public class ValuesController : ControllerBase {
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			Loki.Information("Hallo");
			var tmp = new string[] {"value1", "value2"};
			Loki.ReturnCritical(tmp);
			try
			{
				throw new Exception("FEHLER");
			}
			catch (Exception e)
			{
				Loki.ExceptionCritical(e);
			}
			Loki.SystemCritical("OMG",this);
			return tmp;
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}