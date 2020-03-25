using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulr.Hosting;

namespace Modulr.Samples.Common.Autofac
{
    public class AutofacHostModule : HostModule<ContainerBuilder>
    {
        protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHttpClient();
        }

        protected override void ConfigureContainer(HostBuilderContext context, ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacNativeModule>();
        }
    }
}
