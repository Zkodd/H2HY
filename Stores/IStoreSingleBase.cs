using System;

namespace H2HY.Stores
{
    /// <summary>
    /// Base interface for a simple single store.
    /// Calls Initialised, Set, Changed, Reset.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStoreSingleBase<T>
    {
        /// <summary>
        /// Invoked whenever the store changed.
        /// </summary>
        event Action<StoreSingleEventArgs<T>>? StoreChanged;
    }
}