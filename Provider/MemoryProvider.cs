using System.Collections.Generic;

namespace H2HY.Provider
{
    /// <summary>
    /// Plain memory store. Is using a hashset for storing items.
    /// HashSet T class is mainly designed to do high-performance set operations, such as the intersection 
    /// of two sets intersection, the set of differences and so on. The collection contains a set of elements 
    /// that do not repeat and have no attribute order, and the HashSet rejects duplicate object.
    /// <typeparam name="T"></typeparam>
    /// </summary>
    public class MemoryProvider<T> : IProvider<T>
    {
        private readonly HashSet<T> _memoryList = new HashSet<T>();
        //
        // Summary:
        //     Adds the specified element to a set.
        //
        // Parameters:
        //   item:
        //     The element to add to the set.
        //
        // Returns:
        //     true if the element is added to the System.Collections.Generic.HashSet`1 object;
        //     false if the element is already present.

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="item">item to add</param>
        public void Add(T item)
        {
            _memoryList.Add(item);
        }

        /// <summary>
        /// adds a range of items
        /// </summary>
        /// <param name="items">items to add</param>
        public void AddRange(IEnumerable<T> items)
        {
            _memoryList.UnionWith(items);
        }

        /// <summary>
        /// clears the entire storage.
        /// </summary>
        public void Clear()
        {
            _memoryList.Clear();
        }

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <returns>enumerable of type T of all items</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _memoryList;
        }

        /// <summary>
        /// removes a item from the storage.
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>true on success</returns>
        public bool Remove(T item)
        {
            return _memoryList.Remove(item);
        }

        /// <summary>
        /// unsued in memory provider. NOP.
        /// </summary>
        /// <param name="items"></param>
        public virtual void SaveAll(IEnumerable<T> items)
        {
            //NOP
        }

        /// <summary>
        /// unsued in memory provider. NOP.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(T item)
        {
            //NOP
            return true;
        }
    }
}
