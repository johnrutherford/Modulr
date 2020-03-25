using Microsoft.Extensions.DependencyInjection;

namespace Modulr
{
    /// <summary>
    /// Provides a way to group dependency registrations into a reusable module.
    /// </summary>
    public interface IDependencyModule
    {
        /// <summary>
        /// Add services to the container
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);
    }
}
