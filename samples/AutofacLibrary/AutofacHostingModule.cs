using System;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulr.Hosting;

namespace AutofacLibrary
{
    public class AutofacHostingModule : HostingModule<ContainerBuilder>
    {
        protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Here we need to add services that require using Microsoft.Extensions.DependencyInjection
            services.AddHttpClient();
        }

        protected override void ConfigureContainer(HostBuilderContext context, ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
        }
    }
}
