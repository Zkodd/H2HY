using System.Collections.Generic;

namespace H2HY.Provider
{
    /// <summary>
    /// Generic Providerinterface for all stores.a
    /// Not all provider are handling ids. So the ID interface is not needed for this interface.
    /// But it is possible for some concret provider.
    /// IProvider<T> where T : IIDInterface
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
