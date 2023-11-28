using System;

namespace H2HY.Stores
{
    /// <summary>
    /// Store for a single element which calls:
    /// Initialise, Set, Changed, Reset
    /// </summary>
    public class StoreSingleBase<T> : IStoreSingleBase<T>
    {
        /// <summary>
        /// Store has changed.
        /// </summary>
        public event Action<object, StoreSingleEventArgs<T>>? StoreChanged;

        /// <summary>
        /// Publish to property if needed.
        /// </summary>
        protected T? _currentItem;

        /// <summary>
        /// An item needs initialisation.
        /// </summary>
        /// <param name="item"></param>
        protected void OnInitialise(T item)
        {
            StoreChanged?.Invoke(this, new StoreSingleEventArgs<T>(item, StoreSingleChangedAction.Initialise));
        }

        /// <summary>
        /// A item has been set.
        /// </summary>
        /// <param name="item"></param>
        protected void OnSet(T item)
        {
            StoreChanged?.Invoke(this, new StoreSingleEventArgs<T>(item, StoreSingleChangedAction.Set));
        }

        /// <summary>
        /// the current item has changed
        /// </summary>
        protected void OnChanged(T item)
        {
            StoreChanged?.Invoke(this, new StoreSingleEventArgs<T>(item, StoreSingleChangedAction.Changed));
        }

        /// <summary>
        /// The current store changed dramatically.
        /// </summary>
        protected void OnReset()
        {
            StoreChanged?.Invoke(this, new StoreSingleEventArgs<T>(default, StoreSingleChangedAction.Reset));
        }
    }
}