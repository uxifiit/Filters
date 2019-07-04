using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Common.Extensions
{
    internal static class IEnumerableEx
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source != null)
            {
                foreach (TSource item in source)
                {
                    action.Invoke(item);
                }
            }
        }


        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T item)
        {
            yield return item;
            if (source != null)
            {
                foreach (var element in source)
                {
                    yield return element;
                }
            }
        }


        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T item)
        {
            if (source != null)
            {
                foreach (var element in source)
                {
                    yield return element;
                }
            }

            yield return item;
        }
    }
}
