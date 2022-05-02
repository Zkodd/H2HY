using System.Collections.Generic;

namespace H2HY.Provider
{
    public interface IProvider<T>
    {
        IEnumerable<T> GetAll();
        void SaveAll(IEnumerable<T> items);
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        bool Remove(T item);
        bool Update(T item);
        void Clear();
    }
}
