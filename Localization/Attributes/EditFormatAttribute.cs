using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HyperSlackers.Localization.Extensions;

namespace HyperSlackers.Localization
{
    /// <summary>
    /// Attribute to specify the edit format string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EditFormatAttribute : Attribute, IMetadataAware
    {
        public Type ResourceType { get; set; } 
        public string ResourceName { get; set; }
        private string format;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditFormatAttribute"/> class.
        /// </summary>
        public EditFormatAttribute()
        {
        }

        public EditFormatAttribute(string format)
        {
            this.format = format;
        }

        /// <summary>
        /// Gets or sets the edit format.
        /// </summary>
        /// <value>
        /// The edit format.
        /// </value>
        public string EditFormat
        {
            get
            {
                string resourceString = Helpers.GetResourceString(this.ResourceType, this.ResourceName);

                if (resourceString.IsNullOrWhiteSpace())
                {
                    resourceString = this.format;
                }

                return resourceString;
            }
            set
            {
                this.format = value;
            }
        }

        /// <summary>
        /// When implemented in a class, provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if (!this.format.IsNullOrWhiteSpace())
            {
                metadata.EditFormatString = this.format;
            }
        }
    }
}
