using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace H2HY.Models
{
    /// <summary>
    /// A Fluent List
    /// (WPF) bindable
    /// Using .Subscribe(this) bevor using any .When is mandatory.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class H2H2YFluentList<T> : Collection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        /// <summary>
        /// Static instance of PropertyChangedEventArgs for the "Count" property.
        /// </summary>
        protected static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

        /// <summary>
        /// Static instance of PropertyChangedEventArgs for the indexer property.
        /// </summary>
        protected static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

        /// <summary>
        /// Static instance of NotifyCollectionChangedEventArgs for a reset action.
        /// </summary>
        protected static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

        private readonly Lazy<Dictionary<object, Action<T>>> _added = new();
        private readonly Lazy<Dictionary<object, Action<T>>> _changed = new();
        private readonly Lazy<Dictionary<object, Action<IList<T>>>> _cleared = new();
        private readonly Lazy<Dictionary<object, Action<T>>> _removed = new();
        private object? _lastSubscriber;

        /// <summary>
        /// default constructor
        /// </summary>
        public H2H2YFluentList()
        {
        }

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Occurs when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// informs all subscriber about the changed item. 
        /// Does call WhenChanged but not invoke CollectionChanged
        /// </summary>
        /// <param name="changedItem"></param>
        public void Change(T changedItem)
        {
            NotifyOnItemChanged(changedItem);
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
        /// subscribe as observer - mandatory to call bevor ever When.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public H2H2YFluentList<T> Subscribe(object owner)
        {
            _lastSubscriber = owner;
            return this;
        }

        /// <summary>
        /// Unsubscribe as observer. Unsubscribes from all calls.
        /// </summary>
        /// <param name="owner"></param>
        public void Unsubscribe(object owner)
        {
            if (_added.IsValueCreated)
            {
                _added.Value.Remove(owner);
            }

            if (_cleared.IsValueCreated)
            {
                _cleared.Value.Remove(owner);
            }

            if (_removed.IsValueCreated)
            {
                _removed.Value.Remove(owner);
            }

            if (_changed.IsValueCreated)
            {
                _changed.Value.Remove(owner);
            }
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

            if (added is not null)
            {
                _added.Value.Add(_lastSubscriber, added);
            }
            return this;
        }

        /// <summary>
        /// Occurs when a item has been changed.
        /// </summary>
        /// <param name="changed">added item</param>
        /// <returns></returns>
        public H2H2YFluentList<T> WhenChanged(Action<T> changed)
        {
            if (_lastSubscriber is null)
            {
                throw new Exception("Subscriber is not set.");
            }

            if (changed is not null)
            {
                _changed.Value.Add(_lastSubscriber, changed);
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

            if (cleared is not null)
            {
                _cleared.Value.Add(_lastSubscriber, cleared);
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

            if (removed is not null)
            {
                _removed.Value.Add(_lastSubscriber, removed);
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
        /// Calls al item added observer.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void NotifyOnItemAdded(T item)
        {
            if (_added.IsValueCreated)
            {
                foreach (var action in _added.Value)
                {
                    action.Value(item);
                }
            }
        }

        /// <summary>
        /// Calls all ItemChanged observer.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void NotifyOnItemChanged(T item)
        {
            if (_changed.IsValueCreated)
            {
                foreach (var action in _changed.Value)
                {
                    action.Value(item);
                }
            }
        }

        /// <summary>
        /// Calls all item removed observer.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void NotifyOnItemRemoved(T item)
        {
            if (_removed.IsValueCreated)
            {
                foreach (var action in _removed.Value)
                {
                    action.Value(item);
                }
            }
        }

        /// <summary>
        /// Calls all cleared observer.
        /// </summary>
        /// <param name="items"></param>
        protected virtual void NotifyOnItemsCleared(IList<T> items)
        {
            if (_cleared.IsValueCreated)
            {
                foreach (var action in _cleared.Value)
                {
                    action.Value(items);
                }
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