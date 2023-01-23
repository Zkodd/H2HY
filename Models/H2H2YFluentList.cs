﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace H2HY.Models
{
    /// <summary>
    /// A Fluent Collection - giving a provider is optinal.
    /// (WPF) bindable
    /// Using .Subscribe(this) bevor using any .When is mandatory.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class H2H2YFluentList<T> : Collection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        protected static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

        protected static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

        protected static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

        private object? _lastSubscriber;
        private readonly Dictionary<object, Action<T>> _added = new();
        private readonly Dictionary<object, Action<IList<T>>> _cleared = new();
        private readonly Dictionary<object, Action<T>> _removed = new();

        /// <summary>
        /// default constructor
        /// </summary>
        public H2H2YFluentList()
        {
        }

        /// <summary>
        /// subscribe as observer - has to called bevor ever When.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public H2H2YFluentList<T> Subscripe(object owner)
        {
            _lastSubscriber = owner;
            return this;
        }

        /// <summary>
        /// Unsubscribe as observer. Unsubsricbes from all calls.
        /// </summary>
        /// <param name="owner"></param>
        public void Unsubscribe(object owner)
        {
            _added.Remove(owner);
            _cleared.Remove(owner);
            _removed.Remove(owner);
        }

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Occvurs when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Adds a range of items.
        /// </summary>
        /// <param name="newitems"></param>
        public void AddRange(IEnumerable<T> newitems)
        {
            foreach (var item in newitems)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Clears the current store and loads the given items.
        /// </summary>
        /// <param name="loadedItems">list of new items</param>
        public void LoadAll(IEnumerable<T> loadedItems)
        {
            Clear();
            foreach (var item in loadedItems)
            {
                Add(item);
            }
        }

        /// <summary>
        /// moves the item from the given oldIndex to newIndex.
        /// </summary>
        /// <param name="oldIndex"></param>
        /// <param name="newIndex"></param>
        public void Move(int oldIndex, int newIndex)
        {
            T prev = base[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, prev);

            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Move, prev, newIndex, oldIndex);
        }

        /// <summary>
        /// Occurs when a item has been added.
        /// </summary>
        /// <param name="added">added item</param>
        /// <returns></returns>
        public H2H2YFluentList<T> WhenAdded(Action<T> added)
        {
            if (_lastSubscriber is null)
            {
                throw new Exception("Subscriber is not set.");
            }

            if (added != null)
            {
                _added.Add(_lastSubscriber, added);
            }
            return this;
        }

        /// <summary>
        ///  Occurs when the collection has been cleared.
        /// </summary>
        /// <param name="cleared"></param>
        /// <returns></returns>
        public H2H2YFluentList<T> WhenCleared(Action<IList<T>> cleared)
        {
            if (_lastSubscriber is null)
            {
                throw new Exception("Subscriber is not set.");
            }

            if (cleared != null)
            {
                _cleared.Add(_lastSubscriber, cleared);
            }
            return this;
        }

        /// <summary>
        /// Occurs when a item has been removed.
        /// </summary>
        /// <param name="removed"></param>
        /// <returns></returns>
        public H2H2YFluentList<T> WhenRemoved(Action<T> removed)
        {
            if (_lastSubscriber is null)
            {
                throw new Exception("Subscriber is not set.");
            }

            if (removed != null)
            {
                _removed.Add(_lastSubscriber, removed);
            }
            return this;
        }

        /// <summary>
        /// Clears the entire store and calls: WhenCleared, CountPropertyChanged, IndexerPropertyChanged and ResetCollectionChanged
        /// </summary>
        protected override void ClearItems()
        {
            var items = new List<T>(this);
            base.ClearItems();

            foreach (var item in CollectionsMarshal.AsSpan(items))
            {
                NotifyOnItemRemoved(item);
            }
            NotifyOnItemsCleared(items);

            OnPropertyChanged(CountPropertyChanged);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(ResetCollectionChanged);
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            OnPropertyChanged(CountPropertyChanged);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            NotifyOnItemAdded(item);
        }

        /// <summary>
        /// Calls al itemadded observer.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void NotifyOnItemAdded(T item)
        {
            foreach (var action in _added)
            {
                action.Value(item);
            }
        }

        /// <summary>
        /// Calls all itemremoved observer.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void NotifyOnItemRemoved(T item)
        {
            foreach (var action in _removed)
            {
                action.Value(item);
            }
        }

        /// <summary>
        /// Calls all clearedobserver.
        /// </summary>
        /// <param name="items"></param>
        protected virtual void NotifyOnItemsCleared(IList<T> items)
        {
            foreach (var action in _cleared)
            {
                action.Value(items);
            }
        }

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            => CollectionChanged?.Invoke(this, e);

        /// <summary>
        ///  Occurs when a property value changes.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
            => PropertyChanged?.Invoke(this, args);

        /// <summary>
        /// Removes the item from the collection at the given index.
        /// </summary>
        /// <param name="index">index of item to remove</param>
        protected override void RemoveItem(int index)
        {
            var item = base[index];
            base.RemoveItem(index);

            OnPropertyChanged(CountPropertyChanged);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            NotifyOnItemRemoved(item);
        }

        /// <summary>
        /// Replaces the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, T item)
        {
            T prev = base[index];
            base.SetItem(index, item);

            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, prev, item, index);
            NotifyOnItemRemoved(prev);
            NotifyOnItemAdded(item);
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object? item, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object? item, int index, int oldIndex)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object? oldItem, object? newItem, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
    }
}