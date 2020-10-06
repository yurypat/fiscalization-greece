using Mews.Fiscalization.Greece.Model.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Extensions
{
    internal static class CollectionExtensions
    {
        internal static List<TSource> AsList<TSource>(this IEnumerable<TSource> source)
        {
            return source as List<TSource> ?? source.ToList();
        }

        internal static bool IsSequential<T>(this IReadOnlyList<IIndexedItem<T>> source, int startIndex)
        {
            var expectedIndices = new HashSet<int>(Enumerable.Range(start: startIndex, count: source.Count));
            var actualIndices = source.Select(i => i.Index);
            return expectedIndices.SetEquals(actualIndices);
        }

        internal static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}
