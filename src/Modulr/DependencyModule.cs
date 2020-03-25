using Microsoft.Extensions.DependencyInjection;

namespace Modulr
{
    /// <summary>
    /// Abstract class that implements <see cref="IDependencyModule"/>
    /// </summary>
    public abstract class DependencyModule : IDependencyModule
    {
        /// <summary>
        /// Add services to the container
        /// </summary>
        /// <param name="services"></param>
        public abstract void ConfigureServices(IServiceCollection services);
    }
}
