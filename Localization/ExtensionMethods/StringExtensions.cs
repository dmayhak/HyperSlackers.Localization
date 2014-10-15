using SmartFormat;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSlackers.Localization.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is either null, or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrEmpty(this String value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Returns true if the string is null, empty, or contains only whitespace.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public static bool IsNullOrWhiteSpace(this String value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Adds a space before each upper-case letter except the first.
        /// </summary>
        /// <param name="value">The string to format.</param>
        /// <returns></returns>
        [Pure]
        public static string SpaceOnUpperCase(this string value)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            if (value.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            return string.Join(" ", value.SplitOnUpperCase());
        }

        /// <summary>
        /// Splits the string at upper-case letters.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <returns></returns>
        [Pure]
        private static string[] SplitOnUpperCase(this string value)
        {
            Contract.Ensures(Contract.Result<string[]>() != null);

            if (value.IsNullOrWhiteSpace())
            {
                return new[] { string.Empty };
            }

            if (value.IsAllUpperCase())
            {
                return new[] { value };
            }

            var words = new StringCollection();
            int wordStartIndex = 0;

            char[] letters = value.ToCharArray();
            char previousChar = char.MinValue;

            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current index.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));

                    wordStartIndex = i;
                }

                previousChar = letters[i];
            }

            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            //Copy to a string array.
            var wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);

            // TODO: should we force first word to start with a cap?

            return wordArray;
        }

        /// <summary>
        /// Determines whether a string contains only upper-case letters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        [Pure]
        public static bool IsAllUpperCase(this string value)
        {
            Contract.Requires<ArgumentNullException>(value != null, "value");

            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsLetter(value[i]) && !Char.IsUpper(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Formats the string with the given args using SmartFormat.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        [Pure]
        public static string FormatWith(this string value, params object[] args)
        {
            Contract.Requires<ArgumentNullException>(args != null, "args");
            Contract.Ensures(Contract.Result<string>() != null);

            if (value.IsNullOrWhiteSpace())
            {
                return value ?? string.Empty;
            }

            return FormatWith(value, CultureInfo.CurrentUICulture, args);
        }

        /// <summary>
        /// Formats the string with the given args using SmartFormat.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        [Pure]
        public static string FormatWith(this string value, IFormatProvider provider, params object[] args)
        {
            Contract.Requires<ArgumentNullException>(provider != null, "provider");
            Contract.Requires<ArgumentNullException>(args != null, "args");
            Contract.Ensures(Contract.Result<string>() != null);

            if (value.IsNullOrWhiteSpace())
            {
                return value ?? string.Empty;
            }

            return Smart.Format(provider, value, args);
        }

        
    }
}
