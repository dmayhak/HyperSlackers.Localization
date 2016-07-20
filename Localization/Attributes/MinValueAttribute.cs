using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HyperSlackers.Localization.Extensions;

namespace HyperSlackers.Localization
{
    /// <summary>
    /// Validation attribute to specify a minimum value for a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class MinValueAttribute : ValidationAttribute
    {
        /// <summary>
        /// The minimum value.
        /// </summary>
        public object MinValue { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="MinValueAttribute"/> class from being created.
        /// </summary>
        private MinValueAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinValueAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        public MinValueAttribute(int minValue)
        {
            this.MinValue = minValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinValueAttribute"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        public MinValueAttribute(double minValue)
        {
            this.MinValue = minValue;
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            try
            {
                if (this.MinValue is int)
                {
                    return (int)value >= (int)this.MinValue;
                }
                else
                {
                    return (double)value >= (double)this.MinValue;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            string formatString = this.ErrorMessage;

            if (formatString.IsNullOrWhiteSpace())
            {
                formatString = Helpers.GetResourceString(this.ErrorMessageResourceType, this.ErrorMessageResourceName);
            }

            if (formatString.IsNullOrWhiteSpace())
            {
                formatString = "{0} must be at least {1}.";
            }

            return formatString.FormatWith(name, this.MinValue);
        }
    }
}
