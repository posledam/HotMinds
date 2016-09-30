using System.Collections.Generic;
using System.Linq;

namespace HotMinds.Collections
{
    public static class CommonCollectionExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null) return true;
            var readOnlyCollection = collection as IReadOnlyCollection<T>;
            if (readOnlyCollection != null)
            {
                return readOnlyCollection.Count == 0;
            }
            //var editableCollection = collection as ICollection<T>;
            //if (editableCollection != null)
            //{
            //    return editableCollection.Count == 0;
            //}
            return !collection.Any();
        }
    }
}
