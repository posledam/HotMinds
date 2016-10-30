using System;
using System.Collections.Generic;
using System.Linq;

namespace HotMinds.Extensions
{
    /// <summary>
    ///     Very useful simple LINQ extensions.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        ///     Split sequence to N parts (subsequences).
        /// </summary>
        /// <typeparam name="T">
        ///     Type of sequence elements. </typeparam>
        /// <param name="sequence">
        ///     The original sequence of elements. </param>
        /// <param name="parts">
        ///     Parts count. Must be >= 2. </param>
        /// <returns>
        ///     The sequence of N (<paramref name="parts"/>) subsequences or less.
        /// </returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> sequence, int parts)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if (parts < 2)
                throw new ArgumentOutOfRangeException(nameof(parts), parts, "The number of parts must be at least two.");

            int i = 0;
            var splits = from item in sequence
                         group item by i++ % parts into part
                         select part.AsEnumerable();
            return splits;
        }

        /// <summary>
        ///     Separates a sequence of elements in the portion of a given size.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of sequence elements. </typeparam>
        /// <param name="sequence">
        ///     The original sequence of elements. </param>
        /// <param name="size">
        ///     The number of elements (size) in portion portions. It must be > 0. </param>
        /// <returns>
        ///     Sequence containing the portions with elements count less or equals given size (<paramref name="size"/>).
        /// </returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> sequence, int size)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size), size, "Portion size must be greater than zero.");

            return PartitionInternal(sequence, size);
        }

        private static IEnumerable<IEnumerable<T>> PartitionInternal<T>(this IEnumerable<T> sequence, int size)
        {
            var partition = new List<T>(size);
            foreach (var item in sequence)
            {
                partition.Add(item);
                if (partition.Count == size)
                {
                    yield return partition;
                    partition = new List<T>(size);
                }
            }
            if (partition.Count > 0)
                yield return partition;
        }
    }
}
