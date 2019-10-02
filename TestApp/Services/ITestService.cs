using System.Collections.Generic;
using LokiLogger.WebExtension.ViewModel;
using TestApp.Models;

namespace TestApp.Services
{
    public interface ITestService
    {
        OperationResult<List<TestData>> GetData();
        OperationResult<TestData> NewData(TestData data);
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
    }
}