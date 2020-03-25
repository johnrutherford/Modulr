using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Modulr.Activation;
using Modulr.AspNetCore;

namespace Modulr
{
    public static class WebHostBuilderExtensions
    {
        private static readonly ModuleActivator<IWebHostModule> ModuleActivator = new ModuleActivator<IWebHostModule>();

        public static IWebHostBuilder UseModule(this IWebHostBuilder builder, IWebHostModule module)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            if (module is null)
                throw new ArgumentNullException(nameof(module));

            return module.Configure(builder);
        }

        public static IWebHostBuilder UseModule<TModule>(this IWebHostBuilder builder)
            where TModule : IWebHostModule, new()
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            var module = new TModule();
            return module.Configure(builder);
        }

        public static IWebHostBuilder UseModule(this IWebHostBuilder builder, Type moduleType)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            var module = ModuleActivator.Activate(moduleType);
            return module.Configure(builder);
        }

        public static IWebHostBuilder UseAssemblyModules(this IWebHostBuilder builder, params Assembly[] assemblies)
        {
            return builder.UseAssemblyModules((IEnumerable<Assembly>)assemblies);
        }

        public static IWebHostBuilder UseAssemblyModules(this IWebHostBuilder builder, IEnumerable<Assembly> assemblies)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            var modules = ModuleActivator.ScanAndActivate(assemblies);

            foreach (var module in modules)
            {
                builder = module.Configure(builder);
            }

            return builder;
        }

        public static IWebHostBuilder UseAssemblyModules(this IWebHostBuilder builder, params Type[] assemblyMarkerTypes)
        {
            return builder.UseAssemblyModules((IEnumerable<Type>)assemblyMarkerTypes);
        }

        public static IWebHostBuilder UseAssemblyModules(this IWebHostBuilder builder, IEnumerable<Type> assemblyMarkerTypes)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (assemblyMarkerTypes == null)
                throw new ArgumentNullException(nameof(assemblyMarkerTypes));

            var assemblies = assemblyMarkerTypes
                .Select(x => x.GetTypeInfo().Assembly);

            return builder.UseAssemblyModules(assemblies);
        }
    }
}
