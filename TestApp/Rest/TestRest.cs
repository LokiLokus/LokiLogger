using LokiLogger.WebExtension.Controller;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Rest
{
    [Route("api/Test")]
    public class TestRest:LokiController
    {
        public ITestService TestService { get; set; }

        public TestRest(ITestService service)
        {
            TestService = service;
        }

        [HttpGet("GetData")]
        public IActionResult GetData()
        {
            return CallRest(TestService.GetData);
        }
        [HttpGet("NewData/{data}")]
        public IActionResult GetData([FromRoute]string data)
        {
            return CallRest(TestService.NewData,new TestData(){Name = data});
        }
    }
}