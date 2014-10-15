using HyperSlackers.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HyperSlackers.Localization.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the localized display name for an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDisplayName<TEnum>(this TEnum value)
        {
            ConventionModelMetadataProvider provider = ModelMetadataProviders.Current as ConventionModelMetadataProvider;

            string displayName = value.ToString();
            Type containerType = value.GetType();
            string containerName = containerType.Name;

            if (provider != null)
            {
                Type[] resourceTypes = Helpers.GetResourceTypes(provider.DefaultResourceTypes, null);
                string[] resourceNames = Helpers.GetResourceNames(new string[] { containerName + "_" + displayName + "_Name", containerName + "_" + displayName, displayName + "_Name", displayName }, null);

                HyperSlackers.Localization.ResourceLocation location = Helpers.LocateResource(resourceTypes, resourceNames);
                if (location != null)
                {
                    return Helpers.GetResourceString(location);
                }
            }

            // no resource found....
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DisplayNameAttribute[] attributes = (DisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].DisplayName;
            }
            else // TODO: force a resx lookup?
            {
                return displayName.SpaceOnUpperCase();
            }
        }

        /// <summary>
        /// Gets the localized description for an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescription<TEnum>(this TEnum value)
        {
            ConventionModelMetadataProvider provider = ModelMetadataProviders.Current as ConventionModelMetadataProvider;

            string description = value.ToString();
            Type containerType = value.GetType();
            string containerName = containerType.Name;

            if (provider != null)
            {
                Type[] resourceTypes = Helpers.GetResourceTypes(provider.DefaultResourceTypes, null);
                string[] resourceNames = Helpers.GetResourceNames(new string[] { containerName + "_" + description + "_Description", containerName + "_" + description + "_Tooltip", description + "_Description", description + "_Tooltip" }, null);

                HyperSlackers.Localization.ResourceLocation location = Helpers.LocateResource(resourceTypes, resourceNames);
                if (location != null)
                {
                    return Helpers.GetResourceString(location);
                }
            }

            // no resource found....
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
            {
                return attributes[0].Description;
            }
            else // TODO: force a resx lookup?
            {
                return description.SpaceOnUpperCase();
            }
        }
    }
}
