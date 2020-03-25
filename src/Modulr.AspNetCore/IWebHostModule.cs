using Microsoft.AspNetCore.Hosting;

namespace Modulr.AspNetCore
{
    public interface IWebHostModule
    {
        IWebHostBuilder Configure(IWebHostBuilder builder);
    }
}
