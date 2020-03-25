using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulr.Hosting;

namespace Modulr.Samples.Common
{
    public class MetaModule : HostModule
    {
        protected override IHostBuilder ConfigureBuilder(IHostBuilder builder)
        {
            return builder.UseModule<BasicHostModule>();
        }

        protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddModule<BasicDependencyModule>();
        }
    }
}
