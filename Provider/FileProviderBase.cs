using H2HY.Models;
using System.Collections.Generic;
using System.Linq;

namespace H2HY.Provider
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FileProviderBase<T> : IProvider<T> where T : IIDInterface
    {
        private readonly HashSet<T> _memoryList = new();
        private readonly string _filename;

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        public FileProviderBase(string filename)
        {
            _filename = filename;
        }

        /// <summary>
        /// Adds a item
        /// </summary>
        /// <param name="item">item to ad</param>
        public void Add(T item)
        {
            AddRange(new List<T>() { item });
        }

        /// <summary>
        /// Adds a range of items
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                FileProviderBase<T>.HandleId(item, _memoryList);
                _memoryList.Add(item);
            }
        }

        /// <summary>
        /// clears the db file.
        /// </summary>
        public void Clear()
        {
            _memoryList.Clear();
            SaveModel(_filename, _memoryList);
        }

        /// <summary>
        /// gets all items
        /// </summary>
        /// <returns>all read items.</returns>
        public IEnumerable<T> GetAll()
        {
            _memoryList.Clear();
            LoadModel(_filename, out List<T>? loadedItems);
            if (loadedItems is not null)
            {
                _memoryList.UnionWith(loadedItems);
            }

            return _memoryList;
        }

        /// <summary>
        /// removes given item using its ID.
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>true on success</returns>
        public bool Remove(T item)
        {
            T? itemToRemove = _memoryList.FirstOrDefault(i => i.Id == item.Id);
            if (itemToRemove is not null)
            {
                return _memoryList.Remove(itemToRemove);
            }

            return false;
        }

        /// <summary>
        /// saves all given items.
        /// </summary>
        /// <param name="items">items to save</param>
        public void SaveAll(IEnumerable<T> items)
        {
            SaveModel(_filename, items);
        }

        /// <summary>
        /// not implemented for file-provider. Dont use.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>false</returns>
        public bool Update(T item)
        {
            return false;//we dont do updates using the file system.
        }

        protected abstract void LoadModel(string filename, out List<T>? list);

        protected abstract void SaveModel(string filename, IEnumerable<T> list);

        private static void HandleId(T item, IEnumerable<T> usedItems)
        {
            item.Id = usedItems.Max(i => i.Id) + 1;
            //List<int> ids = new List<int>();
            //usedItems.ToList().ForEach(i => ids.Add(i.Id));

            //item.Id = ids.Count + 1;
            //while (ids.FirstOrDefault(i => i == item.Id) != default)
            //{
            //    item.Id++;
            //}
        }
    }
}