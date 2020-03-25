using System;
using System.Threading.Tasks;

namespace Modulr.Tests.Common
{
    public class TestServiceImplementation : ITestService
    {
        public Task SendAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
}
