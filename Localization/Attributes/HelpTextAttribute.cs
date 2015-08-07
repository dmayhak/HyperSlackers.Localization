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
    /// Attribute to specify help text for a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class HelpTextAttribute : Attribute, IMetadataAware
    {
        public Type ResourceType { get; set; }
        public string ResourceName { get; set; }
        private string helpText;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpTextAttribute"/> class.
        /// </summary>
        public HelpTextAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpTextAttribute"/> class.
        /// </summary>
        /// <param name="helpText">The help text.</param>
        public HelpTextAttribute(string helpText)
        {
            this.helpText = helpText;
        }

        /// <summary>
        /// Gets or sets the help text.
        /// </summary>
        /// <value>
        /// The help text.
        /// </value>
        public string HelpText
        {
            get
            {
                string resourceString = Helpers.GetResourceString(this.ResourceType, this.ResourceName);

                if (resourceString.IsNullOrWhiteSpace())
                {
                    resourceString = this.helpText;
                }

                return resourceString;
            }
            set
            {
                this.helpText = value;
            }
        }

        /// <summary>
        /// When implemented in a class, provides meta-data to the model meta-data creation process.
        /// </summary>
        /// <param name="metadata">The model meta-data.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if (!metadata.AdditionalValues.ContainsKey("HelpText"))
            {
                metadata.AdditionalValues.Add("HelpText", this.HelpText);
            }
        }
    }
}
