using HyperSlackers.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace HyperSlackers.Localization.Extensions
{
    internal static class AttributeExtensions
    {
        /// <summary>
        /// Gets an attribute from the specified type or, if not exists, the types assembly.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static TAttribute GetAttributeOnTypeOrAssembly<TAttribute>(this Type type) where TAttribute : Attribute
        {
            Contract.Requires<ArgumentNullException>(type != null, "type");

            return type.First<TAttribute>() ?? type.Assembly.First<TAttribute>();
        }

        /// <summary>
        /// Gets the first attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="attributeProvider">The attribute provider.</param>
        /// <returns></returns>
        public static TAttribute First<TAttribute>(this ICustomAttributeProvider attributeProvider) where TAttribute : Attribute
        {
            Contract.Requires<ArgumentNullException>(attributeProvider != null, "attributeProvider");

            return attributeProvider.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
        }

        /// <summary>
        /// Localizes the text for the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">Type of the container.</param>
        /// <param name="alternateContainerType">Type of the alternate container.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <returns></returns>
        internal static Attribute Localize(this Attribute attribute, Type containerType, Type alternateContainerType, string propertyName, Type[] defaultResourceTypes)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");

            return Helpers.LocalizeAttribute(attribute, containerType, alternateContainerType, propertyName, defaultResourceTypes);
        }
    }
}