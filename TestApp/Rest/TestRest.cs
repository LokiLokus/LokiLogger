using System;
using System.Threading.Tasks;
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

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            return await CallRest(TestService.Test);
        }
        [HttpGet("Test2")]
        public async Task<IActionResult> Test2()
        {
            return await CallRest(TestService.Test2,"asd");
        }
        [HttpGet("GetData")]
        public IActionResult GetData()
        {
            return CallRest(TestService.GetData);
        }
        [HttpGet("NewData/{data}")]
        public IActionResult GetData([FromRoute]string data)
        {
            return  CallRest(TestService.NewData,new TestData(){Name = data});
        }
    }
}