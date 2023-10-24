using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace H2HY.Models;

/// <summary>
/// Collection which supports fluent syntax.
/// </summary>
public class H2HYFluentCollection<T> : Collection<T>
{
    private readonly Lazy<Dictionary<object, Action<T>>> _added = new();
    private readonly Lazy<Dictionary<object, Action<T>>> _changed = new();
    private readonly Lazy<Dictionary<object, Action<IList<T>>>> _cleared = new();
    private readonly Lazy<Dictionary<object, Action<T>>> _removed = new();
    private object? _lastSubscriber;

    /// <summary>
    /// subscribe as observer - mandatory to call bevor ever When.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public H2HYFluentCollection<T> Subscribe(object owner)
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
    public H2HYFluentCollection<T> WhenAdded(Action<T> added)
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
    public H2HYFluentCollection<T> WhenChanged(Action<T> changed)
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
    public H2HYFluentCollection<T> WhenCleared(Action<IList<T>> cleared)
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
    public H2HYFluentCollection<T> WhenRemoved(Action<T> removed)
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
    /// informs all subscriber about the changed item.
    /// Does call WhenChanged but not invoke CollectionChanged
    /// </summary>
    /// <param name="changedItem"></param>
    public void Change(T changedItem)
    {
        NotifyOnItemChanged(changedItem);
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
            ItemRemoving(item);
            NotifyOnItemRemoved(item);
        }
        NotifyOnItemsCleared(items);
    }

    /// <summary>
    /// Inserts an item at the specified index.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    protected override void InsertItem(int index, T item)
    {
        base.InsertItem(index, item);

        ItemAdding(item);
        NotifyOnItemAdded(item);
    }

    /// <summary>
    /// Removes the item from the collection at the given index.
    /// </summary>
    /// <param name="index">index of item to remove</param>
    protected override void RemoveItem(int index)
    {
        var item = base[index];
        base.RemoveItem(index);

        ItemRemoving(item);
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

        ItemRemoving(prev);
        NotifyOnItemRemoved(prev);

        ItemAdding(item);
        NotifyOnItemAdded(item);
    }


    /// <summary>
    /// Called after an item has been added to the collection
    /// and bevor the observers are called.
    /// </summary>
    /// <param name="item"></param>
    protected virtual void ItemAdding(T item)
    {

    }

    /// <summary>
    /// Called after an item has been removed from the collection
    /// and bevor the observers are called.
    /// </summary>
    /// <param name="item"></param>
    protected virtual void ItemRemoving(T item)
    {

    }

    /// <summary>
    /// Calls al item added observer.
    /// </summary>
    /// <param name="item"></param>
    private void NotifyOnItemAdded(T item)
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
    private void NotifyOnItemChanged(T item)
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
    private void NotifyOnItemRemoved(T item)
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
    private void NotifyOnItemsCleared(IList<T> items)
    {
        if (_cleared.IsValueCreated)
        {
            foreach (var action in _cleared.Value)
            {
                action.Value(items);
            }
        }
    }
}