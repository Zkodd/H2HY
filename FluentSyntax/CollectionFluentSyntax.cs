using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace H2HY.FluentSyntax
{
    public static partial class FluentSyntax
    {
        /// <summary>
        /// finally, a AddRange for a ICollection.. which is using a foreach-loop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (items is List<T> list)
            {
                foreach (T item in CollectionsMarshal.AsSpan(list))
                {
                    collection.Add(item);
                }
            }
            else
            {
                items.ForEach(collection.Add);
            }

            return collection;
        }

        /// <summary>
        /// removes the given items from ICollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ICollection<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach(var item in items)
            {
                collection.Remove(item);
            }

            return collection;
        }

        /// <summary>
        /// removes the first found item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static ICollection<T> RemoveOne<T>(this ICollection<T> collection, Func<T, bool> search)
        {
            if (collection.FirstOrDefault(search) is { } x)
            {
                collection.Remove(x);
            }

            return collection;
        }

        /// <summary>
        /// Removes items where the given lambdafunction returns true.
        /// Calls Changed(item, StoreChanged.Remove) for each item on succsess.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        public static ICollection<T> RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            IEnumerable<T> itemstoremove = collection.Where(i => predicate(i)).ToList();
            return collection.RemoveRange(itemstoremove);
        }
    }
}