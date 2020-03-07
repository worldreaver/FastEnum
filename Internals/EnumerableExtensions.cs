﻿using System;
using System.Collections.Generic;
using System.Linq;



namespace FastEnumUtility.Internals
{
    /// <summary>
    /// Provides <see cref="IEnumerable{T}"/> extension methods.
    /// </summary>
    internal static partial class EnumerableExtensions
    {
        /// <summary>
        /// Gets collection count if <see cref="source"/> is materialized, otherwise null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int? CountIfMaterialized<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source == Enumerable.Empty<T>()) return 0;
            if (source == Array.Empty<T>()) return 0;
            if (source is ICollection<T> a) return a.Count;
            if (source is IReadOnlyCollection<T> b) return b.Count;

            return null;
        }


        /// <summary>
        /// Gets collection if <see cref="source"/> is materialized, otherwise ToArray();ed collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="nullToEmpty"></param>
        public static IEnumerable<T> Materialize<T>(this IEnumerable<T> source, bool nullToEmpty = true)
        {
            if (source == null)
            {
                if (nullToEmpty)
                    return Enumerable.Empty<T>();
                throw new ArgumentNullException(nameof(source));
            }
            if (source is ICollection<T>) return source;
            if (source is IReadOnlyCollection<T>) return source;
            return source.ToArray();
        }
    }
}
