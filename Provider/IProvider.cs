using System.Collections.Generic;

namespace H2HY.Provider
{
    /// <summary>
    /// Generic Providerinterface for all stores.
    /// Not all provider are handling ids. So the ID interface is not needed for this interface.
    /// But it is possible for some concret provider.
    ///
    ///  <![CDATA[ IProvider<T> where T : IIDInterface ]]>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProvider<T>
    {
        /// <summary>
        /// add a item to the storage.
        /// </summary>
        /// <param name="item">item to add</param>
        void Add(T item);

        /// <summary>
        /// adds a range of items to the storage.
        /// </summary>
        /// <param name="items">items to add</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        /// clears the entire storage.
        /// </summary>
        void Clear();

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <returns>readonly enumerable of type T</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// removes a item from the storage.
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>success of remove process</returns>
        bool Remove(T item);

        /// <summary>
        /// saves all given items.
        /// </summary>
        /// <param name="items">items to save</param>
        void SaveAll(IEnumerable<T> items);

        /// <summary>
        /// updates given item. Recomendation: use IIDInterface to identify items by using the id.
        /// </summary>
        /// <param name="item">item to update</param>
        /// <returns>success of update process</returns>
        bool Update(T item);
    }
}
