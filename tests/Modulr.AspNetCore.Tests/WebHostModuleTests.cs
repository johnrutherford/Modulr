using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Xunit;

namespace Modulr.AspNetCore.Tests
{
    public class WebHostModuleTests
    {
        [Fact]
        public void Configure_AllProtectedMethodsCalled()
        {
            var mock = new Mock<WebHostModule>()
            {
                CallBase = true
            };

            var module = mock.Object;
            var builder = new WebHostBuilder();

            var host = builder
                .UseModule(module)
                .UseStartup<TestStartup>()
                .Build();

            host.Should().NotBeNull();

            mock.Protected().Verify<IWebHostBuilder>("ConfigureBuilder", Times.Once(),
                ItExpr.IsAny<IWebHostBuilder>());

            mock.Protected().Verify("ConfigureAppConfiguration", Times.Once(),
                ItExpr.IsAny<WebHostBuilderContext>(), ItExpr.IsAny<IConfigurationBuilder>());

            mock.Protected().Verify("ConfigureServices", Times.Once(),
                ItExpr.IsAny<WebHostBuilderContext>(), ItExpr.IsAny<IServiceCollection>());

            mock.Protected().Verify("ConfigureServices", Times.Once(),
                ItExpr.IsAny<IServiceCollection>());

            mock.VerifyNoOtherCalls();
        }
    }
}
