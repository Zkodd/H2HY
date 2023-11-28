using System;

namespace H2HY.Stores
{
    /// <summary>
    /// Base interface for a simple store.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IStoreBase<T>
    {
        /// <summary>
        /// Invoked whenever the store changed.
        /// </summary>
        event Action<object, StoreEventArgs<T>>? StoreChanged;
    }
}