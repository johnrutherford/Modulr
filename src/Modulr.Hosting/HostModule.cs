using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Modulr.Hosting
{
    /// <summary>
    /// Abstract class for implementing <see cref="IHostModule"/>
    /// </summary>
    public abstract class HostModule : IHostModule
    {
        /// <summary>
        /// Configure the <see cref="IHostBuilder" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IHostBuilder Configure(IHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return ConfigureBuilder(builder)
                .ConfigureHostConfiguration(ConfigureHostConfiguration)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices);
        }

        /// <summary>
        /// Configure the host builder directly. Can be used to add sub-modules or wrap the builder with a decorator class.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected virtual IHostBuilder ConfigureBuilder(IHostBuilder builder) => builder;

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize
        /// the <seealso cref="IHostingEnvironment"/> for use later in the build process.
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual void ConfigureHostConfiguration(IConfigurationBuilder configuration)
        {

        }

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        protected virtual void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configuration)
        {
            
        }

        /// <summary>
        /// Add services to the container.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        protected virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            
        }
    }

    /// <summary>
    /// Abstract class for implementing <see cref="IHostModule"/> with a third-party container.
    /// </summary>
    /// <remarks>
    /// The third-party container must be configured on the <see cref="IHostBuilder" /> 
    /// by calling <see cref="IHostBuilder.UseServiceProviderFactory{TContainerBuilder}(IServiceProviderFactory{TContainerBuilder})" />
    /// </remarks>
    /// <typeparam name="TContainerBuilder"></typeparam>
    public abstract class HostModule<TContainerBuilder> : IHostModule
    {
        public IHostBuilder Configure(IHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return ConfigureBuilder(builder)
                .ConfigureHostConfiguration(ConfigureHostConfiguration)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .ConfigureContainer<TContainerBuilder>(ConfigureContainer);
        }

        /// <summary>
        /// Configure the host builder directly. Can be used to add sub-modules or wrap the builder with a decorator class.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected virtual IHostBuilder ConfigureBuilder(IHostBuilder builder) => builder;

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize
        /// the <seealso cref="IHostingEnvironment"/> for use later in the build process.
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual void ConfigureHostConfiguration(IConfigurationBuilder configuration)
        {

        }

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        protected virtual void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configuration)
        {

        }

        /// <summary>
        /// Add services to the container.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        protected virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {

        }

        /// <summary>
        /// Configure the third-party dependency container.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="builder"></param>
        protected virtual void ConfigureContainer(HostBuilderContext context, TContainerBuilder builder)
        {
            
        }
    }
}
