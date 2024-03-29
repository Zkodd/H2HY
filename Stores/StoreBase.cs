﻿using System;

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
        public event Action<object, StoreEventArgs<T>>? StoreChanged;

        /// <summary>
        /// Publish - if needed.
        /// </summary>
        protected T? _currentItem;

        /// <summary>
        /// Item has been added.
        /// </summary>
        /// <param name="item"></param>
        protected void OnAdded(T item)
        {
            StoreChanged?.Invoke(this, new StoreEventArgs<T>(item, StoreChangedAction.Add));
        }

        /// <summary>
        /// item has been removed
        /// </summary>
        /// <param name="item"></param>
        protected void OnRemoved(T item)
        {
            StoreChanged?.Invoke(this, new StoreEventArgs<T>(item, StoreChangedAction.Remove));
        }

        /// <summary>
        /// the given item has changed
        /// </summary>
        /// <param name="item"></param>
        protected void OnChanged(T item)
        {
            StoreChanged?.Invoke(this, new StoreEventArgs<T>(item, StoreChangedAction.Changed));
        }

        /// <summary>
        /// The current store changed dramatically.
        /// </summary>
        protected void OnReset()
        {
            StoreChanged?.Invoke(this, new StoreEventArgs<T>(default, StoreChangedAction.Reset));
        }
    }
}