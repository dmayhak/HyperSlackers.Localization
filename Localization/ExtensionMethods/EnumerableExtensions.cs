using System;
using System.Collections.Generic;

using System.Linq;

namespace HyperSlackers.Localization.Extensions
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Replaces items (source) in collection with new items (replacement).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="source">The source.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns></returns>
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> collection, T source, T replacement) where T : class
        {
            Helpers.ThrowIfNull(collection != null, "collection");

            var collectionWithout = collection;

            if (source != null)
            {
                collectionWithout = collectionWithout.Except(new[] { source });
            }

            return collectionWithout.Union(new[] { replacement });
        }
    }
}