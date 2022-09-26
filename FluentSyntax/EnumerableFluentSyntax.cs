using System;
using System.Collections.Generic;

namespace H2HY.FluentSyntax
{
    /// <summary>
    /// Some Fluent-syntax for method chaining.
    /// </summary>
    public static partial class FluentSyntax
    {
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
    }
}