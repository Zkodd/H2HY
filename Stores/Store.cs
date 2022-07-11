using H2HY.Provider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H2HY.Stores
{
    /// <summary>
    /// Usage example with using a file provider:
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
        /// <summary>
        /// the used provider for item managment.
        /// </summary>
        protected readonly IProvider<T> _provider;

        /// <summary>
        /// Called on store changes.
        /// </summary>
        public event Action<T, StoreChanged> Changed;

        /// <summary>
        /// All items. (using lazy load)
        /// </summary>
        public IEnumerable<T> Items => _items.Value;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="provider">provider to use to manage item load/save.</param>
        public Store(IProvider<T> provider)
        {
            _provider = provider;

            _items = new Lazy<List<T>>(() =>
            {
                IEnumerable<T> loadedItems = _provider.GetAll();
                List<T> newList = new List<T>();
                newList.AddRange(loadedItems);

                Loaded(newList);
                return newList;
            });
        }

        /// <summary>
        /// updates given item and calls <code>StoreChanged.Changed</code> on success.
        /// </summary>
        /// <param name="item">item to update</param>
        /// <returns>success of operation</returns>
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
        /// Removes items where the given lambdafunction returns true.
        /// Calls Changed(item, StoreChanged.Remove) for each item on succsess.
        /// </summary>
        /// <param name="predicate">Lambdafunction, returns true for removal.</param>
        public void RemoveWhere(Func<T, bool> predicate)
        {
            IEnumerable<T> itemstoremove = Items.Where(i => predicate(i)).ToList();
            foreach (T item in itemstoremove)
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Clears the entire store and calls Changed(default, StoreChanged.Reset)
        /// </summary>
        public void Clear()
        {
            _provider.Clear();
            _items.Value.Clear();
            Changed?.Invoke(default, StoreChanged.Reset);
        }

        /// <summary>
        /// All items have been loaded from the provider.
        /// Do not use Items-property inside. Modify loaded items instead.
        /// virtual - NOP.
        /// </summary>
        protected virtual void Loaded(List<T> loadedItems)
        {
            // override me
        }
    }
}
