# Modulr ![.NET Core](https://github.com/johnrutherford/Modulr/workflows/Build/badge.svg)

Modules for Microsoft.Extensions.DependencyInjection and Microsoft.Extensions.Hosting.

## Dependency Modules

``` csharp
public class MyModule : DependencyModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddSingleton<IApiService, SomeApiService>();
    }
}

public static class Program
{
    public static Main(string[] args)
    {
        var services = new ServiceCollection();
        // Now add the module
        services.AddModule<MyModule>();

        var serviceProvider = services.BuildServiceProvider();

        var api = serviceProvider.GetRequiredService<IApiService>();
        ...
    }
}
```

## Host Modules

```csharp
public class MyHostModule : HostModule
{
    protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddModule<MyModule>();

        if (context.Environment.IsDevelopment())
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });
        }
    }
}

public static class Program
{
    public static Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseModule<MyHostModule>()
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }
}
```
