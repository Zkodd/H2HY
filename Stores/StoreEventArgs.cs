using System;

namespace H2HY.Stores
{
    /// <summary>
    /// Generic Args for store-changed action.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoreEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Affected Value
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val">affected value</param>
        /// <param name="action">performed action</param>
        public StoreEventArgs(T val, NotifyStoreChangedAction action)
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