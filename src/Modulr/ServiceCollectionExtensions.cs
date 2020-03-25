using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Modulr.Activation;

namespace Modulr
{
    /// <summary>
    /// Dependency Module extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static readonly ModuleActivator<IDependencyModule> Activator = new ModuleActivator<IDependencyModule>();

        /// <summary>
        /// Add the service registrations in the specified <see cref="IDependencyModule"/> to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="module">The <see cref="IDependencyModule"/> with service registrations to add</param>
        /// <returns></returns>
        public static IServiceCollection AddModule(this IServiceCollection services, IDependencyModule module)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (module == null)
                throw new ArgumentNullException(nameof(module));

            module.ConfigureServices(services);

            return services;
        }

        /// <summary>
        /// Create an instance of <typeparamref name="TModule"/> and add it to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModule<TModule>(this IServiceCollection services)
            where TModule : IDependencyModule, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var module = new TModule();
            module.ConfigureServices(services);

            return services;
        }

        /// <summary>
        /// Create an instance of the specified module type and add it to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static IServiceCollection AddModule(this IServiceCollection services, Type moduleType)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var module = Activator.Activate(moduleType);
            module.ConfigureServices(services);

            return services;
        }

        /// <summary>
        /// Search the specified assemblies for type that implement <seealso cref="IDependencyModule"/>. 
        /// Create an instance of each and add them to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyModules(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddAssemblyModules((IEnumerable<Assembly>)assemblies);
        }

        /// <summary>
        /// Search the specified assemblies for type that implement <seealso cref="IDependencyModule"/>. 
        /// Create an instance of each and add them to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyModules(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            var modules = Activator.ScanAndActivate(assemblies);

            foreach (var module in modules)
            {
                module.ConfigureServices(services);
            }

            return services;
        }

        /// <summary>
        /// Search the assemblies containing the specified types for types that implement <seealso cref="IDependencyModule"/>. 
        /// Create an instance of each and add them to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyMarkerTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyModules(this IServiceCollection services, params Type[] assemblyMarkerTypes)
        {
            return services.AddAssemblyModules((IEnumerable<Type>)assemblyMarkerTypes);
        }

        /// <summary>
        /// Search the assemblies containing the specified types for types that implement <seealso cref="IDependencyModule"/>. 
        /// Create an instance of each and add them to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyMarkerTypes"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyModules(this IServiceCollection services, IEnumerable<Type> assemblyMarkerTypes)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (assemblyMarkerTypes == null)
                throw new ArgumentNullException(nameof(assemblyMarkerTypes));

            var assemblies = assemblyMarkerTypes
                .Select(t => t.GetTypeInfo().Assembly);

            return services.AddAssemblyModules(assemblies);
        }
    }
}
