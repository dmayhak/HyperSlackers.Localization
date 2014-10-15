using System;
using System.Reflection;

namespace HyperSlackers.Localization.Extensions
{
    internal static class ReflectionExtensions
    {

        /// <summary>
        /// Returns true if the specified type has the specified property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static bool PropertyExists(this Type type, string propertyName)
        {
            if (type == null || propertyName.IsNullOrWhiteSpace())
            {
                return false;
            }

            var property = type.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            if (property == null)
            {
                return false;
            }

            var getter = property.GetGetMethod(true);

            return getter.IsPublic || getter.IsAssembly || getter.IsFamilyOrAssembly;
        }
    }
}