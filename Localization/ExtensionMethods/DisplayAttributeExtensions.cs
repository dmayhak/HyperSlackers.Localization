using HyperSlackers.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HyperSlackers.Localization.Extensions
{
    internal static class DisplayAttributeExtensions
    {
        /// <summary>
        /// Creates a new copy of the specified DisplayAttribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static DisplayAttribute Copy(this DisplayAttribute attribute)
        {
            if (attribute == null)
            {
                return null; 
            }

            // DisplayAttribute is sealed, so it's safe to copy.
            var copy = new DisplayAttribute
            {
                Name = attribute.Name,
                GroupName = attribute.GroupName,
                Description = attribute.Description,
                ResourceType = attribute.ResourceType,
                ShortName = attribute.ShortName,
                Prompt = attribute.Prompt
            };

            return copy;
        }

        /// <summary>
        /// Determines whether this instance of DisplayAttribute can supply a display name.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static bool CanSupplyDisplayName(this DisplayAttribute attribute)
        {
            return attribute != null && attribute.ResourceType != null && !attribute.Name.IsNullOrWhiteSpace();
        }
    }
}