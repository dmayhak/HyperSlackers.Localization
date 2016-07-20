using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using HyperSlackers.Localization.Extensions;

namespace HyperSlackers.Localization
{
    /// <summary>
    /// Custom ModelMetadataProvider to allow for conventions based resource strings.
    /// </summary>
    public class ConventionModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        /// <summary>
        /// The default resource types for string lookups.
        /// </summary>
        public Type[] DefaultResourceTypes { get; private set; } // default resource types to look in
        /// <summary>
        /// Indicates if classes will use the conventions based approach by default (if true) of if classes must specify the MetadataConventionsAttribute to enable it.
        /// </summary>
        public bool RequireConventionAttribute { get; private set; } // if true, only apply to classes that have the MetadataConventionsAttribute applied
        /// <summary>
        /// If true, resource types are searched thoroughly for all resource name variations before moving on to next resource type. If false, resource names are searched for 
        /// in all resource files before checking next (less specific) resource name.
        /// </summary>
        public bool ResourceTypeHasPriority { get; set; }
        /// <summary>
        /// Prefixes to class names that can be ignored when formulating resource names.
        /// </summary>
        public string[] Prefixes { get; set; }
        /// <summary>
        /// Suffixes to class nams that can be ignored when formulating resource names.
        /// </summary>
        public string[] Suffixes { get; set; }

        /// <summary>
        /// Create conventions based provider that requires classes to specify a MetadataConventionsAttribute to enable conventions based resource lookups.
        /// </summary>
        public ConventionModelMetadataProvider()
            : this(true, null)
        {
        }

        /// <summary>
        /// Create conventions based provider that specifies if classes need to specify a MetadataConventionsAttribute to enable conventions based resource lookups.
        /// </summary>
        /// <param name="requireConventionAttribute">If true, requires classes to specify a MetadataConventionsAttribute to enable conventions based resource lookups</param>
        public ConventionModelMetadataProvider(bool requireConventionAttribute)
            : this(requireConventionAttribute, null)
        {
        }

        /// <summary>
        /// Create conventions based provider that specifies if classes need to specify a MetadataConventionsAttribute to enable conventions based resource lookups and specifies
        /// the default resource type for lookups.
        /// </summary>
        /// <param name="requireConventionAttribute">If true, requires classes to specify a MetadataConventionsAttribute to enable conventions based resource lookups</param>
        /// <param name="defaultResourceType">The default resource type</param>
        public ConventionModelMetadataProvider(bool requireConventionAttribute, Type defaultResourceType)
        {
            RequireConventionAttribute = requireConventionAttribute;
            DefaultResourceTypes = defaultResourceType == null ? new Type[] { } : new Type[] { defaultResourceType };
        }

        /// <summary>
        /// Create conventions based provider that specifies if classes need to specify a MetadataConventionsAttribute to enable conventions based resource lookups and specifies
        /// the default resource types for lookups.
        /// </summary>
        /// <param name="requireConventionAttribute">If true, requires classes to specify a MetadataConventionsAttribute to enable conventions based resource lookups</param>
        /// <param name="defaultResourceType">The default resource type</param>
        /// <param name="additionalResourceTypes">Additional default resource types</param>
        public ConventionModelMetadataProvider(bool requireConventionAttribute, Type defaultResourceType, params Type[] additionalResourceTypes)
        {
            Helpers.ThrowIfNull(defaultResourceType != null, "defaultResourceType");
            Helpers.ThrowIfNull(additionalResourceTypes != null, "additionalResourceTypes");

            RequireConventionAttribute = requireConventionAttribute;

            List<Type> types = new List<Type>();
            if (defaultResourceType != null)
            {
                types.Add(defaultResourceType);
            }

            foreach (var item in additionalResourceTypes)
	        {
                if (item != null)
                {
                    types.Add(item);
                }
	        }

            this.DefaultResourceTypes = types.ToArray();
        }

        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// The metadata for the property.
        /// </returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            Type conventionType = containerType ?? modelType;

            var conventionAttribute = conventionType.GetAttributeOnTypeOrAssembly<MetadataConventionsAttribute>();
            if (conventionAttribute == null && this.RequireConventionAttribute)
            {
                // ConventionAttribute is required and not present
                base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            }

            Type alternateConventionType = conventionAttribute == null ? null : conventionAttribute.AlternateContainerType;

            // default resource types
            List<Type> resourceTypes = new List<Type>();
            if (conventionAttribute != null && conventionAttribute.ResourceTypes != null)
            {
                resourceTypes.AddRange(conventionAttribute.ResourceTypes);
            }
            resourceTypes.AddRange(this.DefaultResourceTypes);

            // loop through all attributes and create new attributes where necessary
            var newAttributes = new List<Attribute>();
            foreach (var item in attributes.ToArray())
            {
                newAttributes.Add(item.Localize(conventionType, alternateConventionType, propertyName, resourceTypes.ToArray()));
            }

            // if no DisplayAttribute was specified, create one
            DisplayAttribute displayAttribute = newAttributes.FirstOrDefault(a => a is DisplayAttribute) as DisplayAttribute;
            if (displayAttribute == null && containerType != null)
            {
                displayAttribute = new DisplayAttribute().Localize(conventionType, alternateConventionType, propertyName, resourceTypes.ToArray()) as DisplayAttribute;
                newAttributes.Add(displayAttribute);
            }

            // if no DisplayFormatAttribute was specified, create one
            DisplayFormatAttribute displayFormatAttribute = newAttributes.FirstOrDefault(a => a is DisplayFormatAttribute) as DisplayFormatAttribute;
            if (displayFormatAttribute == null && containerType != null)
            {
                displayFormatAttribute = new DisplayFormatAttribute().Localize(conventionType, alternateConventionType, propertyName, resourceTypes.ToArray()) as DisplayFormatAttribute;
                if (!displayFormatAttribute.NullDisplayText.IsNullOrWhiteSpace() || !displayFormatAttribute.DataFormatString.IsNullOrWhiteSpace())
                {
                    newAttributes.Add(displayFormatAttribute);
                }
            }

            // if no HelpTextAttribute specified, create one (if we find data for it in resx)
            HelpTextAttribute helpTextAttribute = newAttributes.FirstOrDefault(a => a is HelpTextAttribute) as HelpTextAttribute;
            if (helpTextAttribute == null && containerType != null)
            {
                helpTextAttribute = new HelpTextAttribute().Localize(conventionType, alternateConventionType, propertyName, resourceTypes.ToArray()) as HelpTextAttribute;
                if (!helpTextAttribute.HelpText.IsNullOrWhiteSpace())
                {
                    newAttributes.Add(helpTextAttribute);
                }
            }

            // if no EditFormatAttribute specified, create one (if we find data for it in resx)
            EditFormatAttribute editFormatAttribute = newAttributes.FirstOrDefault(a => a is EditFormatAttribute) as EditFormatAttribute;
            if (helpTextAttribute == null && containerType != null)
            {
                editFormatAttribute = new EditFormatAttribute().Localize(conventionType, alternateConventionType, propertyName, resourceTypes.ToArray()) as EditFormatAttribute;
                if (!editFormatAttribute.EditFormat.IsNullOrWhiteSpace())
                {
                    newAttributes.Add(editFormatAttribute);
                }
            }

            // prepare the return value
            ModelMetadata metadata = base.CreateMetadata(newAttributes, containerType, modelAccessor, modelType, propertyName);

            // TODO: is this ok? turn off IsRequired unless the required attribute exists
            if (metadata.IsRequired && newAttributes.FirstOrDefault(a => a is RequiredAttribute) == null)
            {
                metadata.IsRequired = false;
            }

            foreach (var item in newAttributes)
            {
                var mdaAttribute = item as IMetadataAware;
                if (mdaAttribute != null)
                {
                    mdaAttribute.OnMetadataCreated(metadata); // I thought this was automatic....  check on this
                }
            }

            // see if user used non-DisplayAttribute stuff...
            if (metadata.Description.IsNullOrWhiteSpace())
            {
                DescriptionAttribute description = newAttributes.FirstOrDefault(a => a is DescriptionAttribute) as DescriptionAttribute;
                if (description != null)
                {
                    metadata.Description = description.Description;
                }
            }

            if (metadata.DisplayName.IsNullOrWhiteSpace())
            {
                DisplayNameAttribute displayName = newAttributes.FirstOrDefault(a => a is DisplayNameAttribute) as DisplayNameAttribute;
                if (displayName != null)
                {
                    metadata.DisplayName = displayName.DisplayName; // TODO: do we want to do this?
                }
            }

            // other types we want to add
            if (containerType != null)
            {
                // TODO: DisplayFormatString
                // TODO: EditFormatString
                // TODO: NullDisplayText
                // TODO: popover title and text?
            }

            // last chance to have a "decent" display name
            if (metadata.DisplayName == null || metadata.DisplayName == metadata.PropertyName)
            {
                metadata.DisplayName = metadata.PropertyName.SpaceOnUpperCase();
            }

            return metadata;
        }
    }
}
