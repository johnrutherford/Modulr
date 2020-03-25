using Microsoft.Extensions.Hosting;

namespace Modulr.Hosting
{
    /// <summary>
    /// Provides a way to put host configuration into a re-usable module.
    /// </summary>
    public interface IHostModule
    {
        /// <summary>
        /// Configure the <see cref="IHostBuilder" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>Returns the same <see cref="IHostBuilder"/> or another instance of <see cref="IHostBuilder"/> that decorates the original.</returns>
        IHostBuilder Configure(IHostBuilder builder);
    }
}
