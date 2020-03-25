using Microsoft.Extensions.DependencyInjection;

namespace Modulr.Tests.Common
{
    public class TestDependencyModule : DependencyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ITestService, TestServiceImplementation>();
        }
    }
}
