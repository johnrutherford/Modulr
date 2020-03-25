using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulr.Hosting;

namespace Modulr.Samples.Common
{
    public class BasicHostModule : HostModule
    {
        protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}
