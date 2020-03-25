using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Modulr.Activation
{
    /// <summary>
    /// Helper class for activating modules
    /// </summary>
    /// <typeparam name="TModuleInterface"></typeparam>
    public class ModuleActivator<TModuleInterface>
    {
        /// <summary>
        /// Create an instance of the provided module type. 
        /// </summary>
        /// <param name="moduleType">Non-abstract type with a public default constructor, implementing <see cref="TModuleInterface"/></param>
        /// <returns>Constructed module</returns>
        public virtual TModuleInterface Activate(Type moduleType)
        {
            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));

            if (!moduleType.IsAssignableTo<TModuleInterface>())
                throw new ArgumentException($"Type {moduleType.FullName} must implement {typeof(TModuleInterface).FullName}", nameof(moduleType));

            return (TModuleInterface)Activator.CreateInstance(moduleType);
        }

        /// <summary>
        /// Scan the provided assemblies for non-abstract types implementing <see cref="TModuleInterface"/>. Create an instance of each.
        /// </summary>
        /// <param name="assemblies">Assemblies to scan for module types</param>
        /// <returns>Constructed modules</returns>
        public virtual IEnumerable<TModuleInterface> ScanAndActivate(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            return assemblies
                .Distinct()
                .SelectMany(a => a.DefinedTypes)
                .Where(t => !t.IsAbstract && t.IsAssignableTo<TModuleInterface>())
                .Select(t => (TModuleInterface)Activator.CreateInstance(t));
        }
    }
}
