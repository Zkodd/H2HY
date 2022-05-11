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

        public FileProvider(string filename)
        {
            _filename = filename;
        }

        public void Add(T item)
        {
            AddRange(new List<T>() { item });
        }

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

        public void Clear()
        {
            List<T> emptyList = new List<T>();
            SaveModel(_filename, emptyList);
        }

        public IEnumerable<T> GetAll()
        {
            LoadModel(_filename, out List<T> loadedAreas);
            return loadedAreas;
        }

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

        public void SaveAll(IEnumerable<T> items)
        {
            SaveModel(_filename, new List<T>(items));
        }

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
            while (ids.FirstOrDefault(i => i == item.Id) != default) { item.Id++; }
        }
    }
}
