using System;

namespace Modulr.Activation
{
    internal static class TypeExtensions
    {
        public static bool IsAssignableTo<T>(this Type type)
            => typeof(T).IsAssignableFrom(type);
    }
}
