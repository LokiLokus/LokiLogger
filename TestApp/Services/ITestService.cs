using System;
using System.Collections.Generic;
using LokiLogger.WebExtension.ViewModel;
using TestApp.Models;

namespace TestApp.Services
{
    public interface ITestService
    {
        OperationResult<List<TestData>> GetData();
        OperationResult<TestData> NewData(TestData data);
        OperationResult<string> Throw();
    }
    
    public class TestService:ITestService{
        public OperationResult<List<TestData>> GetData()
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
            return OperationResult<List<TestData>>.Success(result);
        }

        public OperationResult<TestData> NewData(TestData data)
        {
            data.Id = "2";
            return OpRes.Success(data);
        }

        public OperationResult<string> Throw()
        {
            throw new Exception("This is an Exception");
        }
    }
}