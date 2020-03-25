using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modulr.AspNetCore
{
    public abstract class WebHostModule : IWebHostModule
    {
        public IWebHostBuilder Configure(IWebHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return ConfigureBuilder(builder)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((Action<WebHostBuilderContext, IServiceCollection>)ConfigureServices)
                .ConfigureServices((Action<IServiceCollection>)ConfigureServices);
        }

        protected virtual IWebHostBuilder ConfigureBuilder(IWebHostBuilder builder) => builder;

        protected virtual void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configuration)
        {
            
        }

        protected virtual void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {

        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
