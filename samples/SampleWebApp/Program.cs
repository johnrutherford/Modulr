using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Modulr;
using Modulr.Samples.Common;

namespace SampleWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseModule<MetaModule>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
