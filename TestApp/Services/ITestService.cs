using System;
using System.Collections.Generic;
using LokiLogger.WebExtension.ViewModel;
using TestApp.Models;

namespace TestApp.Services
{
    public interface ITestService
    {
        Result<List<TestData>> GetData();
        Result<TestData> NewData(TestData data);
        Result<string> Throw();
    }
    
    public class TestService:ITestService{
        public Result<List<TestData>> GetData()
        {
            var result = new List<TestData>()
            {
                new TestData()
                {
                    Id = "1",
                    Name = "ASdhasdas"
                },
                new TestData()
                {
                    Id = "2",
                    Name = "sldfds"
                }
            };
            return Result.Ok(result);
        }

        public Result<TestData> NewData(TestData data)
        {
            data.Id = "2";
            return Result.Ok(data);
        }

        public Result<string> Throw()
        {
            throw new Exception("This is an Exception");
        }
    }
}