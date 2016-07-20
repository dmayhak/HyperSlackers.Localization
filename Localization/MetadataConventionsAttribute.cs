using System;
using System.Collections.Generic;


namespace HyperSlackers.Localization
{
    /// <summary>
    /// Specifies that the class should use conventions based resource lookups and optionally specifies additional resource types to use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)] // TODO: apply to  | AttributeTargets.Module | AttributeTargets.Assembly as well?
    public class MetadataConventionsAttribute : Attribute
    {
        /// <summary>
        /// Additional resource types to use for lookups.
        /// </summary>
        public Type[] ResourceTypes { get; private set; }
        /// <summary>
        /// Specifies an alternate container type name to use in place of the actual class name.
        /// </summary>
        public Type AlternateContainerType { get; set; }

        /// <summary>
        /// Specifies that the class should use conventions based resource lookups.
        /// </summary>
        public MetadataConventionsAttribute()
        {
        }

        /// <summary>
        /// Specifies that the class should use conventions based resource lookups and specifies additional resource types.
        /// Resource types specified here take priority over the default ones.
        /// </summary>
        /// <param name="resourceTypes">Additional resource types for lookups</param>
        public MetadataConventionsAttribute(params Type[] resourceTypes)
        {
            Helpers.ThrowIfNull(resourceTypes != null, "resourceTypes");

            List<Type> types = new List<Type>();

            foreach (var item in resourceTypes)
            {
                types.Add(item);
            }

            this.ResourceTypes = types.ToArray();
        }
    }
}