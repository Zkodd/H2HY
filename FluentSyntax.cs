using System;
using System.Collections.Generic;
using System.Linq;

namespace H2HY.FluentSyntax
{
    /// <summary>
    /// Some Fluent-syntax for method chaining.
    /// </summary>
    public static class FluentSyntax
    {
        //public static void Then<T>(this T caller, Action<T> action)
        //    => action?.Invoke(caller);

        /// <summary>
        /// Shorted if-then syntax
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Then(this bool condition, Action action)
        {
            if (condition)
            {
                action();
            }

            return condition;
        }

        /// <summary>
        /// shorted if-then-else syntax
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool Else(this bool condition, Action action)
        {
            if (!condition)
            {
                action();
            }

            return condition;
        }


        /// <summary>
        /// shorted ForEach-syntax.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection is IList<T> list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    action(list[i]);
                }
            }
            else
            {
                foreach (var item in collection)
                {
                    action(item);
                }
            }

            return collection;
        }

        /// <summary>
        /// finally, a AddRange for a ICollection.. which is using a foreach-loop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            items.ForEach(collection.Add);
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
            items.ForEach(v => collection.Remove(v));
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
        public static void RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            IEnumerable<T> itemstoremove = collection.Where(i => predicate(i)).ToList();
            collection.RemoveRange(itemstoremove);
        }
    }
}
