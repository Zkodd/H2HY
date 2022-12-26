using System;

namespace H2HY.Stores
{
    /// <summary>
    /// Indicates a perfomed action on an sinlge item.
    /// </summary>
    public enum NotifyStoreChangedAction
    {
        /// <summary>
        /// An item was added to the collection.
        /// </summary>
        Add = 0,

        /// <summary>
        /// An item was removed from the collection.
        /// </summary>
        Remove = 1,

        /// <summary>
        /// The item has changed.
        /// </summary>
        Changed = 2,

        /// <summary>
        /// The store has changed drasticly.
        /// </summary>
        Reset = 3,
    }

    /// <summary>
    /// Generic Args for store-changed action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoreEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Affected Value
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val">affected value</param>
        /// <param name="action">performed action</param>
        public StoreEventArgs(T? val, NotifyStoreChangedAction action)
        {
            Value = val;
            Action = action;
        }

        /// <summary>
        /// performed action
        /// </summary>
        public NotifyStoreChangedAction Action { get; }
    }
}