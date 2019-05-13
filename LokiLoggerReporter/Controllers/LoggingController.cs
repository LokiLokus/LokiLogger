using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Log([FromBody] List<Log> model)
        {
            try
            {
                if (model != null)
                {
                    _dbContext.AddRange(model);
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetLog")]
        public IActionResult GetLog()
        {
            return Ok(_dbContext.Logs.ToList());
        }

        [HttpGet("GetDebug")]
        public IActionResult GetDebug()
        {
            var tmp = new List<Log>()
            {
                new Log()
                {
                    Class = "asdsad",
                    ID = 1,
                    Line = 134,
                    Data = "[]",
                    Exception = null,
                    LogLevel = LogLevel.Information,
                    LogTyp = LogTyp.Normal,
                    Message = "Funktion wurde aufgerufen",
                    Method = "CallApi",
                    Name = "LokiLogger"
                },
                new Log()
                {
                    Class = "ApiController",
                    ID = 2,
                    Line = 20,
                    Data = "[]",
                    Exception = null,
                    LogLevel = LogLevel.Information,
                    LogTyp = LogTyp.Normal,
                    Message = "Funktion wurde aufgerufen",
                    Method = "CallApi",
                    Name = "LokiLogger"
                }
            };
            return Ok(tmp);
        }
    }
}