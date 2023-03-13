using System;

namespace H2HY.Stores
{
    /// <summary>
    /// Base Store class
    /// Calls a typed StoreEventArgs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoreBase<T> : IStoreBase<T>
    {
        /// <summary>
        /// Store has changed.
        /// </summary>
        public event Action<StoreEventArgs<T>>? StoreChanged;

        /// <summary>
        /// Publish to property if needed.
        /// </summary>
        protected T? _currentItem;

        /// <summary>
        /// Item has been added.
        /// </summary>
        /// <param name="item"></param>
        protected void OnAdded(T item)
        {
            StoreChanged?.Invoke(new StoreEventArgs<T>(item, NotifyStoreChangedAction.Add));
        }

        /// <summary>
        /// item has been removed
        /// </summary>
        /// <param name="item"></param>
        protected void OnRemoved(T item)
        {
            StoreChanged?.Invoke(new StoreEventArgs<T>(item, NotifyStoreChangedAction.Remove));
        }

        /// <summary>
        /// the current item has changed
        /// </summary>
        /// <param name="item"></param>
        protected void OnChanged(T item)
        {
            StoreChanged?.Invoke(new StoreEventArgs<T>(item, NotifyStoreChangedAction.Changed));
        }

        /// <summary>
        /// The current store chandeg dramaticly.
        /// </summary>
        protected void OnReset()
        {
            StoreChanged?.Invoke(new StoreEventArgs<T>(default, NotifyStoreChangedAction.Reset));
        }

    }
}