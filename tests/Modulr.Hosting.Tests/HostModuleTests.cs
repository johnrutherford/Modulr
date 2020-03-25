using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Moq.Protected;
using Xunit;

namespace Modulr.Hosting.Tests
{
    public class HostModuleTests
    {
        [Fact]
        public void Configure_AllProtectedMethodsCalled()
        {
            var mock = new Mock<HostModule>()
            {
                CallBase = true
            };

            var module = mock.Object;
            var builder = new HostBuilder();

            var host = builder
                .UseModule(module)
                .Build();

            host.Should().NotBeNull();

            mock.Protected().Verify<IHostBuilder>("ConfigureBuilder", Times.Once(), exactParameterMatch: false,
                ItExpr.IsAny<IHostBuilder>());
            mock.Protected().Verify("ConfigureHostConfiguration", Times.Once(),
                ItExpr.IsAny<IConfigurationBuilder>());
            mock.Protected().Verify("ConfigureAppConfiguration", Times.Once(), exactParameterMatch: false,
                ItExpr.IsAny<HostBuilderContext>(), ItExpr.IsAny<IConfigurationBuilder>());
            mock.Protected().Verify("ConfigureServices", Times.Once(), exactParameterMatch: false,
                ItExpr.IsAny<HostBuilderContext>(), ItExpr.IsAny<IServiceCollection>());

            mock.VerifyNoOtherCalls();
        }
    }
}
