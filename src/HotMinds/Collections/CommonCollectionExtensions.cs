using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace HotMinds.Collections
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
    }
}
