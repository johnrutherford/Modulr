using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Moq.Protected;
using Xunit;

namespace Modulr.Hosting.Tests
{
    public class AutofacHostModuleTests
    {
        [Fact]
        public void Configure_AllProtectedMethodsCalled()
        {
            var mock = new Mock<HostModule<ContainerBuilder>>()
            {
                CallBase = true
            };

            var module = mock.Object;
            var builder = new HostBuilder();

            var host = builder
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseModule(module)
                .Build();

            host.Should().NotBeNull();

            mock.Protected().Verify<IHostBuilder>("ConfigureBuilder", Times.Once(),
                ItExpr.IsAny<IHostBuilder>());

            mock.Protected().Verify("ConfigureHostConfiguration", Times.Once(),
                ItExpr.IsAny<IConfigurationBuilder>());

            mock.Protected().Verify("ConfigureAppConfiguration", Times.Once(),
                ItExpr.IsAny<HostBuilderContext>(), ItExpr.IsAny<IConfigurationBuilder>());

            mock.Protected().Verify("ConfigureServices", Times.Once(),
                ItExpr.IsAny<HostBuilderContext>(), ItExpr.IsAny<IServiceCollection>());

            mock.Protected().Verify("ConfigureContainer", Times.Once(),
                ItExpr.IsAny<HostBuilderContext>(), ItExpr.IsAny<ContainerBuilder>());

            mock.VerifyNoOtherCalls();
        }
    }
}
