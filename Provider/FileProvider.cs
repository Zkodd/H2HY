using H2HY.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace H2HY.Provider
{
    /// <summary>
    /// Provider for write and save model into xml files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileProvider<T> : IProvider<T> where T : IIDInterface
    {
        private readonly string _filename;

        /// <summary>
        /// creates a fileprovider. All items will be saved/loaded from the given filename.
        /// </summary>
        /// <param name="filename">filename</param>
        public FileProvider(string filename)
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
            List<T> newList = new List<T>(GetAll());
            foreach (T item in items)
            {
                HandleId(item, newList);
                newList.Add(item);
            }

            SaveModel(_filename, newList);
        }

        /// <summary>
        /// clears the db file.
        /// </summary>
        public void Clear()
        {
            List<T> emptyList = new List<T>();
            SaveModel(_filename, emptyList);
        }

        /// <summary>
        /// gets all items
        /// </summary>
        /// <returns>all read items.</returns>
        public IEnumerable<T> GetAll()
        {
            LoadModel(_filename, out List<T> loadedAreas);
            return loadedAreas;
        }

        /// <summary>
        /// removes given item using its ID.
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>true on success</returns>
        public bool Remove(T item)
        {
            IEnumerable<T> loadedAreas = GetAll();
            List<T> newList = new List<T>(loadedAreas);

            if (newList.Remove(newList.FirstOrDefault(i => i.Id == item.Id)))
            {
                SaveModel(_filename, newList);
                return true;
            }

            return false;
        }

        /// <summary>
        /// saves all given items.
        /// </summary>
        /// <param name="items">items to save</param>
        public void SaveAll(IEnumerable<T> items)
        {
            SaveModel(_filename, new List<T>(items));
        }

        /// <summary>
        /// not implemented for file-provider. Dont use.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>false</returns>
        public bool Update(T item)
        {
            return false;//we dont do updates using the file system.
            //IEnumerable<T> loadedAreas = GetAll();
            //List<T> newList = new List<T>(loadedAreas);

            //if (newList.Remove(newList.FirstOrDefault(i => i.Id == item.Id)))
            //{
            //    newList.Add(item);
            //    SaveModel(_filename, newList);
            //    return true;
            //}

            //return false;
        }

        private static void LoadModel(string filename, out List<T> areas)
        {
            //An exception is thrown but handled by the XmlSerializer,
            //so if you just ignore it everything should continue fine.
            XmlSerializer reader = new XmlSerializer(typeof(List<T>));

            if (File.Exists(filename))
            {
                StreamReader file = new StreamReader(filename);
                areas = reader.Deserialize(file) as List<T>;
                file.Close();
            }
            else
            {
                areas = new List<T>();
            }
        }

        private static void SaveModel(string filename, List<T> areas)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<T>));
            FileStream outputfile = File.Create(filename);
            writer.Serialize(outputfile, areas);
            outputfile.Close();
        }

        private void HandleId(T item, IEnumerable<T> usedItems)
        {
            List<int> ids = new List<int>();
            usedItems.ToList().ForEach(i => ids.Add(i.Id));

            item.Id = ids.Count() + 1;
            while (ids.FirstOrDefault(i => i == item.Id) != default) 
            {
                item.Id++; 
            }
        }
    }
}
