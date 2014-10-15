using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HyperSlackers.Localization.Extensions;

namespace HyperSlackers.Localization
{
    internal static class Helpers
    {
        /// <summary>
        /// Retrieves a resource string given the type and name for the resource.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public static string GetResourceString(Type resourceType, string resourceName)
        {
            if (resourceType != null && !resourceName.IsNullOrWhiteSpace())
	        {
                try
                {
                    PropertyInfo prop = resourceType.GetProperty("ResourceManager", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    System.Resources.ResourceManager resMgr = prop.GetValue(null) as System.Resources.ResourceManager;

                    if (resMgr != null)
                    {
                        string resourceString = resMgr.GetString(resourceName);

                        if (!resourceString.IsNullOrWhiteSpace())
                        {
                            return resourceString;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
	        }

            return null;
        }

        /// <summary>
        /// Retrieves a resource string given the resource location (type and name).
        /// </summary>
        /// <param name="location">The resource location.</param>
        /// <returns></returns>
        public static string GetResourceString(ResourceLocation location)
        {
            Contract.Requires<ArgumentNullException>(location != null, "location");

            return GetResourceString(location.ResourceType, location.ResourceName);
        }

        /// <summary>
        /// Gets the resource types for lookup by combining default resource types with (prepended) attribute resource type.
        /// </summary>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <param name="attributeResourceType">Type of the attribute resource.</param>
        /// <returns></returns>
        public static Type[] GetResourceTypes(Type[] defaultResourceTypes, Type attributeResourceType)
        {
            Contract.Requires<ArgumentNullException>(defaultResourceTypes != null, "defaultResourceTypes");

            List<Type> resourceTypes = new List<Type>();
            if (attributeResourceType != null)
            {
                resourceTypes.Add(attributeResourceType);
            }
            resourceTypes.AddRange(defaultResourceTypes);

            return resourceTypes.ToArray();
        }

        /// <summary>
        /// Locates the resource given the (in priority order) resource types and names.
        /// </summary>
        /// <param name="resourceTypes">The resource types to look through.</param>
        /// <param name="resourceNames">The resource names to attempt to match.</param>
        /// <returns></returns>
        public static ResourceLocation LocateResource(Type[] resourceTypes, string[] resourceNames)
        {
            Contract.Requires<ArgumentNullException>(resourceTypes != null, "resourceTypes");
            Contract.Requires<ArgumentNullException>(resourceNames != null, "resourceNames");

            ConventionModelMetadataProvider provider = ModelMetadataProviders.Current as ConventionModelMetadataProvider;

            if (provider.ResourceTypeHasPriority)
            {
                // resource type has highest priority
                foreach (Type resourceType in resourceTypes)
                {
                    foreach (string resourceName in resourceNames)
                    {
                        if (resourceType.PropertyExists(resourceName))
                        {
                            return new ResourceLocation { ResourceType = resourceType, ResourceName = resourceName };
                        }
                    }
                }
            }
            else
            {
                // resource name has highest priority
                foreach (string resourceName in resourceNames)
                {
                    foreach (Type resourceType in resourceTypes)
                    {
                        if (resourceType.PropertyExists(resourceName))
                        {
                            return new ResourceLocation { ResourceType = resourceType, ResourceName = resourceName };
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the potential resource names to lookup by prepending the resource name from the attribute (if specified) to the default list.
        /// </summary>
        /// <param name="defaultResourceNames">The default resource names.</param>
        /// <param name="attributeResourceName">Name of the attribute resource.</param>
        /// <returns></returns>
        public static string[] GetResourceNames(string[] defaultResourceNames, string attributeResourceName)
        {
            Contract.Requires<ArgumentNullException>(defaultResourceNames != null, "defaultResourceNames");
            Contract.Ensures(Contract.Result<string[]>() != null);

            List<string> resourceNames = new List<string>();
            if (attributeResourceName != null)
            {
                resourceNames.Add(attributeResourceName);
            }
            resourceNames.AddRange(defaultResourceNames);

            return resourceNames.ToArray();
        }

        /// <summary>
        /// Localizes an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">The attribute's container type.</param>
        /// <param name="alternateContainerType">The alternate container tpe to use for lookup.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <returns></returns>
        public static Attribute LocalizeAttribute(Attribute attribute, Type containerType, Type alternateContainerType, string propertyName, Type[] defaultResourceTypes)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");


            // TODO: localize DisplayFormatString, EditFormatString, ShortDisplayName, SimpleDisplayText
            // TODO: what is TemplateHint used for?
            // TODO: plain old DisplayAttribute (which is sealed -- bummer)

            ValidationAttribute validationAttribute = attribute as ValidationAttribute;
            if (validationAttribute != null)
            {
                return LocalizeValidationAttribute(validationAttribute, containerType, alternateContainerType, propertyName, defaultResourceTypes);
            }

            DisplayAttribute displayAttribute = attribute as DisplayAttribute;
            if (displayAttribute != null)
            {
                return LocalizeDisplayAttribute(displayAttribute, containerType, alternateContainerType, propertyName, defaultResourceTypes);
            }

            DisplayFormatAttribute displayFormatAttribute = attribute as DisplayFormatAttribute;
            if (displayFormatAttribute != null)
            {
                return LocalizeDisplayFormatAttribute(displayFormatAttribute, containerType, alternateContainerType, propertyName, defaultResourceTypes);
            }

            EditFormatAttribute editFormatAttribute = attribute as EditFormatAttribute;
            if (editFormatAttribute != null)
            {
                return LocalizeEditFormatAttribute(editFormatAttribute, containerType, alternateContainerType, propertyName, defaultResourceTypes);
            }

            Type[] resourceTypes;
            string[] resourceNames;

            HelpTextAttribute helpTextAttribute = attribute as HelpTextAttribute;
            if (helpTextAttribute != null)
            {
                resourceTypes = GetResourceTypes(defaultResourceTypes, helpTextAttribute.ResourceType);
                resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, helpTextAttribute.HelpText, "HelpText");

                ResourceLocation location = LocateResource(resourceTypes, resourceNames);
                if (location != null)
                {
                    helpTextAttribute.ResourceType = location.ResourceType;
                    helpTextAttribute.ResourceName = location.ResourceName;
                    helpTextAttribute.HelpText = GetResourceString(location);
                }

                return helpTextAttribute;
            }

            // nothing we handle here....
            return attribute;
        }

        /// <summary>
        /// Localizes the validation attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">Type of the container.</param>
        /// <param name="alternateContainerType">Type of the alternate container.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <returns></returns>
        public static ValidationAttribute LocalizeValidationAttribute(ValidationAttribute attribute, Type containerType, Type alternateContainerType, string propertyName, Type[] defaultResourceTypes)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");
            Contract.Requires<ArgumentNullException>(containerType != null, "containerType");
            Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");
            Contract.Requires<ArgumentException>(!propertyName.IsNullOrWhiteSpace());
            Contract.Requires<ArgumentNullException>(defaultResourceTypes != null, "defaultResourceTypes");

            Type[] resourceTypes = GetResourceTypes(defaultResourceTypes, attribute.ErrorMessageResourceType);
            string[] resourceNames = GetResourceNames(GetValidationResourceNames(attribute, containerType, alternateContainerType, propertyName), attribute.ErrorMessageResourceName);

            ResourceLocation location = LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.ErrorMessage = Helpers.GetResourceString(location);
                attribute.ErrorMessageResourceType = null;
                attribute.ErrorMessageResourceName = null;

                //attribute.ErrorMessageResourceType = location.ResourceType;
                //attribute.ErrorMessageResourceName = location.ResourceName;
            }

            return attribute;
        }

        /// <summary>
        /// Gets the base name of the container. (i.e. strip off common prefix/suffix strings like View, List, Model, etc.)
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns></returns>
        public static string GetBaseContainerName(string containerName)
        {
            if (containerName.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (containerName.StartsWith("View"))
            {
                containerName = containerName.Substring(4);
            }
            else if (containerName.StartsWith("List"))
            {
                containerName = containerName.Substring(4);
            }
            else if (containerName.StartsWith("Edit"))
            {
                containerName = containerName.Substring(4);
            }
            else if (containerName.StartsWith("Create"))
            {
                containerName = containerName.Substring(6);
            }

            if (containerName.EndsWith("Model"))
            {
                containerName = containerName.Substring(0, containerName.Length - 5);
            }

            return containerName;
        }

        /// <summary>
        /// Gets possible names for a validation attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">The attribute's container type.</param>
        /// <param name="alternateContainerType">Alternate container type to use.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static string[] GetValidationResourceNames(this ValidationAttribute attribute, Type containerType, Type alternateContainerType, string propertyName)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");
            Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");
            Contract.Requires<ArgumentException>(!propertyName.IsNullOrWhiteSpace());

            string shortName = attribute.GetType().Name.Replace("Attribute", string.Empty);
            List<string> suffixes = new List<string>();
            List<string> resourceNames = new List<string>();
            string baseContainerName = null;

            if (containerType != null)
            {
                string baseName = GetBaseContainerName(containerType.Name);
                if (baseName != containerType.Name)
                {
                    baseContainerName = baseName;
                }
            }

            // in some cases, we might want additional suffixes (like the Prompt/Watermark/Placeholder ones)
            StringLengthAttribute stringLengthAttribute = attribute as StringLengthAttribute;
            if (stringLengthAttribute != null)
            {
                if (stringLengthAttribute.MinimumLength <= 0)
                {
                    suffixes.Add(shortName + "Max");
                }
            }
            suffixes.Add(shortName);

            // start with most specific type: Container_Property_AttributeType
            if (containerType != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(containerType.Name + "_" + propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            // second most specific type: AltContainer_Property_AttributeType
            if (alternateContainerType != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(alternateContainerType.Name + "_" + propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            // third most specific type: BaseContainerName_Property_AttributeType
            if (baseContainerName != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(baseContainerName + "_" + propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            // next most specific: Property_AttributeType
            foreach (var suffix in suffixes)
            {
                resourceNames.Add(propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
            }

            // fallback specific types
            if (containerType != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(containerType.Name + "_Error" + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            if (alternateContainerType != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(alternateContainerType.Name + "_Error" + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            if (baseContainerName != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(baseContainerName + "_Error" + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            // last fallback options
            foreach (var suffix in suffixes)
            {
                resourceNames.Add("Error" + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
            }

            return resourceNames.ToArray();
        }

        /// <summary>
        /// Localizes a display attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">The attribute's container type.</param>
        /// <param name="alternateContainerType">Alternate container type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <returns></returns>
        public static DisplayAttribute LocalizeDisplayAttribute(this DisplayAttribute attribute, Type containerType, Type alternateContainerType, string propertyName, Type[] defaultResourceTypes)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");
            Contract.Requires<ArgumentNullException>(defaultResourceTypes != null, "defaultResourceTypes");
            Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");
            Contract.Requires<ArgumentException>(!propertyName.IsNullOrWhiteSpace());

            //DisplayAttribute attribute = new DisplayAttribute
            //{
            //    Name = attribute.Name,
            //    GroupName = attribute.GroupName,
            //    Description = attribute.Description,
            //    ResourceType = null,
            //    ShortName = attribute.ShortName,
            //    Prompt = attribute.Prompt
            //};

            Type[] resourceTypes = Helpers.GetResourceTypes(defaultResourceTypes, attribute.ResourceType);
            string[] resourceNames;
            ResourceLocation location;

            // Name
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.Name, "Name", ""); // "" is because this is default resource item for key
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.Name = Helpers.GetResourceString(location);
            }

            // GroupName
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.GroupName, "GroupName", "Group");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.GroupName = Helpers.GetResourceString(location);
            }
            
            // Description (Tooltip)
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.Description, "Description", "Tooltip");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.Description = Helpers.GetResourceString(location);
            }

            // ShortName
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.ShortName, "ShortName", "Short");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.ShortName = Helpers.GetResourceString(location);
            }

            // Prompt (Placeholder/Watermark)
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.Prompt, "Prompt", "Placeholder", "Watermark");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.Prompt = Helpers.GetResourceString(location);
            }

            attribute.ResourceType = null;

            return attribute;
        }

        /// <summary>
        /// Localizes a display format attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">The attribute's container type.</param>
        /// <param name="alternateContainerType">Alternate container type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <returns></returns>
        public static DisplayFormatAttribute LocalizeDisplayFormatAttribute(this DisplayFormatAttribute attribute, Type containerType, Type alternateContainerType, string propertyName, Type[] defaultResourceTypes)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");
            Contract.Requires<ArgumentNullException>(defaultResourceTypes != null, "defaultResourceTypes");
            Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");
            Contract.Requires<ArgumentException>(!propertyName.IsNullOrWhiteSpace());

            //DisplayFormatAttribute attribute = new DisplayFormatAttribute
            //{
            //    ApplyFormatInEditMode = true,
            //    ConvertEmptyStringToNull = true,
            //    DataFormatString = "",
            //    HtmlEncode = false,
            //    NullDisplayText = ""
            //};

            Type[] resourceTypes = defaultResourceTypes;
            string[] resourceNames;
            ResourceLocation location;

            // DataFormatString
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.DataFormatString, "Format", "DisplayFormat");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                if (location.ResourceName.EndsWith("DisplayFormat"))
                {
                    attribute.ApplyFormatInEditMode = false;
                }
                else // "Format" applies to both (unless overridden by a "EditFormat" entry
                {
                    attribute.ApplyFormatInEditMode = true;
                }

                attribute.DataFormatString = Helpers.GetResourceString(location);
            }

            // NullDisplayText
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.NullDisplayText, "NullDisplayText", "NullText");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.NullDisplayText = Helpers.GetResourceString(location);
            }

            return attribute;
        }

        /// <summary>
        /// Localizes an edit format attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="containerType">The attribute's container type.</param>
        /// <param name="alternateContainerType">Alternate container type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="defaultResourceTypes">The default resource types.</param>
        /// <returns></returns>
        public static EditFormatAttribute LocalizeEditFormatAttribute(this EditFormatAttribute attribute, Type containerType, Type alternateContainerType, string propertyName, Type[] defaultResourceTypes)
        {
            Contract.Requires<ArgumentNullException>(attribute != null, "attribute");
            Contract.Requires<ArgumentNullException>(defaultResourceTypes != null, "defaultResourceTypes");
            Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");
            Contract.Requires<ArgumentException>(!propertyName.IsNullOrWhiteSpace());

            //EditFormatAttribute attribute = new EditFormatAttribute
            //{
            //};

            Type[] resourceTypes = defaultResourceTypes;
            string[] resourceNames;
            ResourceLocation location;

            // DataFormatString
            resourceNames = GetResourceNames(containerType, alternateContainerType, propertyName, attribute.EditFormat, "Format", "DisplayFormat");
            location = Helpers.LocateResource(resourceTypes, resourceNames);
            if (location != null)
            {
                attribute.EditFormat = Helpers.GetResourceString(location);
            }

            return attribute;
        }

        /// <summary>
        /// Gets the potential resource names.
        /// </summary>
        /// <param name="containerType">The attribute's container type.</param>
        /// <param name="alternateContainerType">Alternate container type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="suffixes">The suffixes.</param>
        /// <returns></returns>
        private static string[] GetResourceNames(Type containerType, Type alternateContainerType, string propertyName, string resourceName, params string[] suffixes)
        {
            Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");
            Contract.Requires<ArgumentException>(!propertyName.IsNullOrWhiteSpace());
            Contract.Requires<ArgumentNullException>(suffixes != null, "suffixes");

            List<string> resourceNames = new List<string>();
            string baseContainerName = null;

            if (containerType != null)
            {
                string baseName = GetBaseContainerName(containerType.Name);
                if (baseName != containerType.Name)
                {
                    baseContainerName = baseName;
                }
            }

            // look for specific name first!
            if (!resourceName.IsNullOrWhiteSpace())
            {
                if (containerType != null)
                {
                    resourceNames.Add(containerType.Name + "_" + resourceName);
                }

                if (alternateContainerType != null)
                {
                    resourceNames.Add(alternateContainerType.Name + "_" + resourceName);
                }

                if (baseContainerName != null)
                {
                    resourceNames.Add(baseContainerName + "_" + resourceName);
                }

                resourceNames.Add(resourceName);
            }

            if (containerType != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(containerType.Name + "_" + propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            if (alternateContainerType != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(alternateContainerType.Name + "_" + propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            if (baseContainerName != null)
            {
                foreach (var suffix in suffixes)
                {
                    resourceNames.Add(baseContainerName + "_" + propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
                }
            }

            foreach (var suffix in suffixes)
            {
                resourceNames.Add(propertyName + (suffix.IsNullOrWhiteSpace() ? string.Empty : "_" + suffix));
            }

            return resourceNames.ToArray();
        }
    }
}
