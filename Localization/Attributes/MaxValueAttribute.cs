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
    /// Validation attribute to specify a maximum value for a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class MaxValueAttribute : ValidationAttribute
    {
        /// <summary>
        /// The maximum value.
        /// </summary>
        public object MaxValue { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="MaxValueAttribute"/> class from being created.
        /// </summary>
        private MaxValueAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxValueAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MaxValueAttribute(int maxValue)
        {
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxValueAttribute"/> class.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public MaxValueAttribute(double maxValue)
        {
            this.MaxValue = maxValue;
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
                if (this.MaxValue is int)
                {
                    return (int)value <= (int)this.MaxValue;
                }
                else
                {
                    return (double)value <= (double)this.MaxValue;
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
                formatString = "{0} can be at most {1}.";
            }

            return formatString.FormatWith(name, this.MaxValue);
        }
    }
}
