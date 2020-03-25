using System.Threading.Tasks;

namespace Modulr.Tests.Common
{
    public interface ITestService
    {
        Task SendAsync(string message);
    }
}
