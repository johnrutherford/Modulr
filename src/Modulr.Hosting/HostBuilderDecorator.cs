using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Modulr.Hosting
{
    /// <summary>
    /// Decorator class for <see cref="IHostBuilder"/>.
    /// </summary>
    /// <remarks>
    /// Allows overriding the <see cref="Build()"/> method to extend the inner builder just before or after the <seealso cref="IHost"/> is built.
    /// </remarks>
    public abstract class HostBuilderDecorator : IHostBuilder
    {
        protected IHostBuilder Inner { get; private set; }

        public IDictionary<object, object> Properties => this.Inner.Properties;

        public HostBuilderDecorator(IHostBuilder inner)
        {
            this.Inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public virtual IHost Build()
        {
            return this.Inner.Build();
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            this.Inner = this.Inner.ConfigureAppConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            this.Inner = this.Inner.ConfigureContainer(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            this.Inner = this.Inner.ConfigureHostConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            this.Inner = this.Inner.ConfigureServices(configureDelegate);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            this.Inner = this.Inner.UseServiceProviderFactory(factory);
            return this;
        }

#if !NETSTANDARD2_0
        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            this.Inner = this.Inner.UseServiceProviderFactory(factory);
            return this;
        }
#endif
    }
}
