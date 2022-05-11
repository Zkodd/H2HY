using System.Collections.Generic;

namespace H2HY.Provider
{
    /// <summary>
    /// Plain memory store. Is using a hashset for storing items.
    /// HashSet <T> class is mainly designed to do high-performance set operations, such as the intersection 
    /// of two sets intersection, the set of differences and so on. The collection contains a set of elements 
    /// that do not repeat and have no attribute order, and the HashSet rejects duplicate object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MemoryProvider<T> : IProvider<T>
    {
        private HashSet<T> _memoryList = new HashSet<T>();

        public void Add(T item)
        {
            _memoryList.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _memoryList.UnionWith(items);
        }

        public void Clear()
        {
            _memoryList.Clear();
        }

        public IEnumerable<T> GetAll()
        {
            return _memoryList;
        }

        public bool Remove(T item)
        {
            return _memoryList.Remove(item);
        }

        public void SaveAll(IEnumerable<T> items)
        {
            //NOP
        }

        public bool Update(T item)
        {
            //NOP
            return true;
        }
    }
}
