using System;

namespace H2HY.FluentSyntax;

/// <summary>
/// Some Fluent-syntax for method chaining.
/// </summary>
public static partial class FluentSyntax
{
    //public static void Then<T>(this T caller, Action<T> action)
    //    => action?.Invoke(caller);

    /// <summary>
    /// Shorted if-then syntax.
    /// Example:  (5 > 4).Then(() =>window.Show());
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
    /// Example: (5 > 4).Else(() =>window.Show());
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
}