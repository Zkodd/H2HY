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
        public event Action<StoreSingleEventArgs<T>>? StoreChanged;

        /// <summary>
        /// Publish to property if needed.
        /// </summary>
        protected T? _currentItem;

        /// <summary>
        /// An item needs intialisation.
        /// </summary>
        /// <param name="item"></param>
        protected void OnInitilise(T item)
        {
            StoreChanged?.Invoke(new StoreSingleEventArgs<T>(item, StoreSingleChangedAction.Initialise));
        }

        /// <summary>
        /// A item has been set.
        /// </summary>
        /// <param name="item"></param>
        protected void OnSet(T item)
        {
            StoreChanged?.Invoke(new StoreSingleEventArgs<T>(item, StoreSingleChangedAction.Set));
        }

        /// <summary>
        /// the current item has changed
        /// </summary>
        protected void OnChanged(T item)
        {
            StoreChanged?.Invoke(new StoreSingleEventArgs<T>(item, StoreSingleChangedAction.Changed));
        }

        /// <summary>
        /// The current store chandeg dramaticly.
        /// </summary>
        protected void OnReset()
        {
            StoreChanged?.Invoke(new StoreSingleEventArgs<T>(default, StoreSingleChangedAction.Reset));
        }
    }
}