using H2HY.Provider;
using System;
using System.Collections;
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
    public class Store<T> : ICollection<T>
    {
        /// <summary>
        /// Callback validator. Is called for every added item.
        /// </summary>
        public Func<T, bool>? IsValid;

        /// <summary>
        /// Used provider for item managment.
        /// </summary>
        protected readonly IProvider<T> _provider;

        private readonly Lazy<List<T>> _items;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="provider">provider which manages items.</param>
        public Store(IProvider<T> provider)
        {
            _provider = provider;

            _items = new Lazy<List<T>>(() =>
            {
                IEnumerable<T> loadedItems = _provider.GetAll();
                List<T> newList = new List<T>();
                newList.AddRange(loadedItems);

                PreprocessLoadedList(newList);
                return newList;
            });
        }

        /// <summary>
        /// Called on store changes.
        /// </summary>
        public event Action<T?, StoreChanged>? Changed;
        /// <summary>
        /// The number of elements contained in the store.
        /// </summary>
        public int Count => _items.Value.Count;

        /// <summary>
        /// Is always false.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// All items. (using lazy load)
        /// </summary>
        public IEnumerable<T> Items => _items.Value;
        /// <summary>
        /// Add a item to the list and calls Changed(item, StoreChanged.Add)
        /// </summary>
        /// <param name="newitem"></param>
        public void Add(T newitem)
        {
            if (IsValid is null)
            {
                _provider.Add(newitem);
                _items.Value.Add(newitem);
                Changed?.Invoke(newitem, StoreChanged.Add);
            }
            else
            {
                if (IsValid(newitem))
                {
                    _provider.Add(newitem);
                    _items.Value.Add(newitem);
                    Changed?.Invoke(newitem, StoreChanged.Add);
                }
            }
        }

        /// <summary>
        /// Adds a range of items. Calls IsValid and calls Changed(item, StoreChanged.Add) for each item.
        /// </summary>
        /// <param name="newitems"></param>
        public void AddRange(IEnumerable<T> newitems)
        {
            if (IsValid is null)
            {
                _provider.AddRange(newitems);
                _items.Value.AddRange(newitems);

                foreach (T item in newitems)
                {
                    Changed?.Invoke(item, StoreChanged.Add);
                }
            }
            else
            {
                List<T> validItems = new();
                validItems.AddRange(from T item in newitems
                                    where IsValid(item)
                                    select item);

                _provider.AddRange(validItems);
                _items.Value.AddRange(validItems);

                foreach (T item in validItems)
                {
                    Changed?.Invoke(item, StoreChanged.Add);
                }
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
        /// Determines whether an element is in the
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _items.Value.Contains(item);
        }

        /// <summary>
        /// Copies the entire store to a compatible one-dimensional
        ///  array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">target index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.Value.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///  Returns an enumerator that iterates through the store.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.Value.GetEnumerator();
        }

        /// <summary>
        ///  Returns an enumerator that iterates through the store.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.Value.GetEnumerator();
        }

        /// <summary>
        /// Clears the current store and loads the given items.
        /// </summary>
        /// <param name="items">list of new items</param>
        public void LoadAll(IEnumerable<T> items)
        {
            _items.Value.Clear();

            List<T> newItems = new(items);
            PreprocessLoadedList(newItems);

            _items.Value.AddRange(newItems);
            Changed?.Invoke(default, StoreChanged.Reset);
        }

        /// <summary>
        /// Saves all items.
        /// </summary>
        public void SaveAll()
        {
            SaveAll(_provider);
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
        /// Updates given item using the provider. Calls <code>StoreChanged.Changed</code> on success.
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
        /// All items have been loaded from the provider.
        /// Do not use Items-property inside. Modify <code>newList</code> instead.
        /// virtual - NOP.
        /// </summary>
        protected virtual void PreprocessLoadedList(List<T> newList)
        {
            return;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the store.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            if (_items.Value.Remove(item))
            {
                _provider.Remove(item);
                Changed?.Invoke(item, StoreChanged.Remove);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}