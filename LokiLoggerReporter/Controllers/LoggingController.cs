using System.Collections.Generic;
using System.Linq;
using LokiLoggerReporter.Models;
using Microsoft.AspNetCore.Mvc;

namespace LokiLoggerReporter.Controllers
{
    [ApiController]
    [Route("/api/Logging")]
    public class LoggingController:Controller
    {
        private readonly DatabaseContext _dbContext;
        public LoggingController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("Log")]
        public IActionResult Log([FromBody] List<Log> model)
        {
            if (model != null)
            {
                _dbContext.AddRange(model);
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("GetLog")]
        public IActionResult GetLog()
        {
            return Ok(_dbContext.Logs.ToList());
        }
    }
}