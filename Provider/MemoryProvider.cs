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


        //to do: change interface from void to bool

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
