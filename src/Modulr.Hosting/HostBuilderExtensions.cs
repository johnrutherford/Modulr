using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Modulr.Activation;
using Modulr.Hosting;

namespace Modulr
{
    /// <summary>
    /// Host Module extension methods for <see cref="IHostBuilder"/>
    /// </summary>
    public static class HostBuilderExtensions
    {
        private static readonly ModuleActivator<IHostModule> ModuleActivator = new ModuleActivator<IHostModule>();

        /// <summary>
        /// Use the specified <see cref="IHostModule" /> to configure the specified <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static IHostBuilder UseModule(this IHostBuilder builder, IHostModule module)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (module == null)
                throw new ArgumentNullException(nameof(module));

            return module.Configure(builder);
        }

        /// <summary>
        /// Create an instance of the specified module type and use it to configure the specified <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static IHostBuilder UseModule(this IHostBuilder builder, Type moduleType)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var module = ModuleActivator.Activate(moduleType);
            return module.Configure(builder);
        }

        /// <summary>
        /// Create an instance of <typeparamref name="TModule"/> and use it to configure the specified <see cref="IHostBuilder"/>.
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder UseModule<TModule>(this IHostBuilder builder)
            where TModule : IHostModule, new()
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var module = new TModule();
            return module.Configure(builder);
        }

        /// <summary>
        /// Search the specified assemblies for type that implement <seealso cref="IHostModule"/>. 
        /// Create an instance of each and use them to configure the specified <see cref="IHostBuilder" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IHostBuilder UseAssemblyModules(this IHostBuilder builder, params Assembly[] assemblies)
        {
            return builder.UseAssemblyModules((IEnumerable<Assembly>)assemblies);
        }

        /// <summary>
        /// Search the specified assemblies for type that implement <seealso cref="IHostModule"/>. 
        /// Create an instance of each and use them to configure the specified <see cref="IHostBuilder" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IHostBuilder UseAssemblyModules(this IHostBuilder builder, IEnumerable<Assembly> assemblies)
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

        /// <summary>
        /// Search the assemblies containing the specified types for types that implement <seealso cref="IHostModule"/>.
        /// Create an instance of each and use them to configure the specified <see cref="IHostBuilder" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblyMarkerTypes"></param>
        /// <returns></returns>
        public static IHostBuilder UseAssemblyModules(this IHostBuilder builder, params Type[] assemblyMarkerTypes)
        {
            return builder.UseAssemblyModules((IEnumerable<Type>)assemblyMarkerTypes);
        }

        /// <summary>
        /// Search the assemblies containing the specified types for types that implement <seealso cref="IHostModule"/>.
        /// Create an instance of each and use them to configure the specified <see cref="IHostBuilder" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblyMarkerTypes"></param>
        /// <returns></returns>
        public static IHostBuilder UseAssemblyModules(this IHostBuilder builder, IEnumerable<Type> assemblyMarkerTypes)
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
