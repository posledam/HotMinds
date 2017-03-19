using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;

namespace HotMinds.Extensions
{
    /// <summary>
    ///     Common collections (enumerable) extensions.
    /// </summary>
    public static class CommonCollectionExtensions
    {
        /// <summary>
        ///     Check if collection (enumerable) is null or empty.
        /// </summary>
        /// <param name="collection">Source collection.</param>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <returns>True, if <paramref name="collection"/> is null or empty.</returns>
        public static bool IsEmpty<T>([CanBeNull] this IEnumerable<T> collection)
        {
            if (collection == null) return true;
            var readOnlyCollection = collection as IReadOnlyCollection<T>;
            if (readOnlyCollection != null)
            {
                return readOnlyCollection.Count == 0;
            }
            return !collection.Any();
        }

        /// <summary>
        ///     Determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="value">A sequence in which to locate a value.</param>
        /// <param name="source">The value to locate in the sequence.</param>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        public static bool In<T>([CanBeNull] this T value, [CanBeNull] params T[] source)
        {
            if (source == null || source.Length == 0) return false;
            return source.Contains(value);
        }

        /// <summary>
        ///     Get value from dictionary by key or default value. Supports only IReadOnlyDictionary implementations.
        /// </summary>
        /// <param name="dictionary">Source dictionary, can be null.</param>
        /// <param name="key">Search key, cannot be null.</param>
        /// <param name="defaultValue">Default value, can be null.</param>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>
        ///     Returns specified default value, if soucre dictionaty is null or key not found in source dictionary.
        /// </returns>
        [CanBeNull]
        public static TValue GetOrDefault<TKey, TValue>([CanBeNull] this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, [NotNull] TKey key,
            TValue defaultValue = default(TValue))
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (dictionary == null) return defaultValue;
            var readOnlyDictionary = dictionary as IReadOnlyDictionary<TKey, TValue>;
            if (readOnlyDictionary != null)
            {
                TValue value;
                if (readOnlyDictionary.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            else
            {
                // IDictionary does not inherit IReadOnlyDictionary, so we need this workaround
                var iDictionary = dictionary as IDictionary<TKey, TValue>;
                if (iDictionary != null)
                {
                    TValue value;
                    if (iDictionary.TryGetValue(key, out value))
                    {
                        return value;
                    }
                }
                // the heaviest case...
                else
                {
                    foreach (var keyValuePair in dictionary)
                    {
                        if (keyValuePair.Key.Equals(key))
                        {
                            return keyValuePair.Value;
                        }
                    }
                }
            }
            return defaultValue;
        }

        /// <summary>
        ///     Get <paramref name="enumerable"/> collection as <see cref="IReadOnlyCollection{T}"/> interface reference.
        /// </summary>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <param name="enumerable">Source collection.</param>
        [CanBeNull]
        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }
            var collection = enumerable as IReadOnlyCollection<T>;
            return collection ?? new ReadOnlyCollection<T>(enumerable.ToList());
        }

        /// <summary>
        ///     Get <paramref name="enumerable"/> collection as <see cref="AsReadOnlyList{T}"/> interface reference.
        /// </summary>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <param name="enumerable">Source collection.</param>
        [CanBeNull]
        public static IReadOnlyList<T> AsReadOnlyList<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }
            var collection = enumerable as IReadOnlyList<T>;
            return collection ?? new ReadOnlyCollection<T>(enumerable.ToList());
        }

        /// <summary>
        ///     Get <paramref name="enumerable"/> collection as <see cref="IReadOnlyCollection{T}"/> interface reference or empty
        ///     collection, if source is null.
        /// </summary>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <param name="enumerable">Source collection.</param>
        [NotNull]
        public static IReadOnlyCollection<T> AsReadOnlyCollectionOrEmpty<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return Array.Empty<T>();
            }
            var collection = enumerable as IReadOnlyCollection<T>;
            return collection ?? new ReadOnlyCollection<T>(enumerable.ToList());
        }

        /// <summary>
        ///     Get <paramref name="enumerable"/> collection as <see cref="AsReadOnlyList{T}"/> interface reference or empty
        ///     collection, if source is null.
        /// </summary>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <param name="enumerable">Source collection.</param>
        [NotNull]
        public static IReadOnlyList<T> AsReadOnlyListOrEmpty<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return Array.Empty<T>();
            }
            var collection = enumerable as IReadOnlyList<T>;
            return collection ?? new ReadOnlyCollection<T>(enumerable.ToList());
        }
    }
}
