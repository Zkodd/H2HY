using H2HY.Provider;
using System;
using System.Collections.Generic;

namespace H2HY.Stores
{
    /// <summary>
    /// Usage Example:
    /// 
    /// <![CDATA[ 
    /// services.AddSingleton<FaultStore>();
    /// IProvider<T> where T : IIDInterface
    /// services.AddTransient<IProvider<FaultModel>>(s => new FileProvider<FaultModel>("Faults.xml")) ]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Store<T>
    {
        private readonly Lazy<List<T>> _items;
        protected readonly IProvider<T> _provider;

        public event Action<T, StoreChanged> Changed;

        public IEnumerable<T> Items => _items.Value;

        public Store(IProvider<T> provider)
        {
            _provider = provider;

            _items = new Lazy<List<T>>(() =>
            {
                IEnumerable<T> loadedItems = _provider.GetAll();
                List<T> newList = new List<T>();
                newList.AddRange(loadedItems);
                return newList;
            });
        }

        public bool Update(T item)
        {
            if (_provider.Update(item))
            {
                Changed?.Invoke(item, StoreChanged.Changed);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Loads all items.
        /// </summary>
        public IEnumerable<T> LoadAll()
        {
            return Items;
        }

        /// <summary>
        /// Saves the entire current list. 
        /// </summary>
        public void SaveAll()
        {
            _provider.SaveAll(Items);
        }

        /// <summary>
        /// Add a item to the list and calls Changed(item, StoreChanged.Add)
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            _provider.Add(item);
            _items.Value.Add(item);
            Changed?.Invoke(item, StoreChanged.Add);
        }

        /// <summary>
        /// Adds a range of items and calls Changed(item, StoreChanged.Add) for each item.
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            _provider.AddRange(items);
            _items.Value.AddRange(items);

            foreach (T item in items)
            {
                Changed?.Invoke(item, StoreChanged.Add);
            }
        }

        /// <summary>
        /// Trys to removes the given item. Calls Changed(item, StoreChanged.Remove) on succsess.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            if (_items.Value.Remove(item))
            {
                _provider.Remove(item);
                Changed?.Invoke(item, StoreChanged.Remove);
            }
        }

        /// <summary>
        /// Clears the entire list and calls Changed(item, StoreChanged.Reset)
        /// </summary>
        public void Clear()
        {
            _provider.Clear();
            _items.Value.Clear();
            Changed?.Invoke(default, StoreChanged.Reset);
        }
    }
}
