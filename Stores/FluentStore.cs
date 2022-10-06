using H2HY.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace H2HY.Stores
{
    /// <summary>
    /// A Fluent Collection - giving a provider is optinal.
    /// (WPF) bindable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FluentStore<T> : Collection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        protected static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

        protected static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

        protected static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

        /// <summary>
        /// the used provider for item managment.
        /// </summary>
        protected readonly IProvider<T>? _provider;

        private readonly List<Action<T>> _added = new();
        private readonly List<Action<IList<T>>> _cleared = new();
        private readonly List<Action<T>> _removed = new();

        /// <summary>
        /// default constructor
        /// </summary>
        public FluentStore()
        {
        }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="provider">provider which manages items.</param>
        public FluentStore(IProvider<T> provider) : base()
        {
            foreach (var item in provider.GetAll())
            {
                Add(item);
            }
            _provider = provider;
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
            _provider?.AddRange(newitems);
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
        /// Saves all items.
        /// </summary>
        public void SaveAll()
        {
            if (_provider is not null)
            {
                SaveAll(_provider);
            }
        }

        /// <summary>
        /// Saves the current list using the given provider.
        /// </summary>
        /// <param name="provider">Used provider. SaveAll(items) will be called.</param>
        public void SaveAll(IProvider<T> provider)
        {
            provider.SaveAll(Items);
        }

        /// <summary>
        /// Occurs when a item has been added.
        /// </summary>
        /// <param name="added">added item</param>
        /// <returns></returns>
        public FluentStore<T> WhenAdded(Action<T> added)
        {
            if (added != null)
            {
                _added.Add(added);
            }
            return this;
        }

        /// <summary>
        ///  Occurs when the collection has been cleared.
        /// </summary>
        /// <param name="cleared"></param>
        /// <returns></returns>
        public FluentStore<T> WhenCleared(Action<IList<T>> cleared)
        {
            if (cleared != null)
            {
                _cleared.Add(cleared);
            }
            return this;
        }

        /// <summary>
        /// Occurs when a item has been removed.
        /// </summary>
        /// <param name="removed"></param>
        /// <returns></returns>
        public FluentStore<T> WhenRemoved(Action<T> removed)
        {
            if (removed != null)
            {
                _removed.Add(removed);
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
            _provider?.Clear();

            if (_cleared.Count > 0)
            {
                NotifyOnItemsCleared(items);
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    NotifyOnItemRemoved(items[i]);
                }
            }

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
            _provider?.Add(item);

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
            for (int i = 0; i < _added.Count; i++)
            {
                _added[i](item);
            }
        }

        /// <summary>
        /// Calls all itemremoved observer.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void NotifyOnItemRemoved(T item)
        {
            for (int i = 0; i < _removed.Count; i++)
            {
                _removed[i](item);
            }
        }

        /// <summary>
        /// Calls all clearedobserver.
        /// </summary>
        /// <param name="items"></param>
        protected virtual void NotifyOnItemsCleared(IList<T> items)
        {
            for (int i = 0; i < _cleared.Count; i++)
            {
                _cleared[i](items);
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
            _provider?.Remove(item);

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

            _provider?.Remove(prev);
            _provider?.Add(item);

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