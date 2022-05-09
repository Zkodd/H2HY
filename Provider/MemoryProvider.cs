using System.Collections.Generic;

namespace H2HY.Provider
{
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
