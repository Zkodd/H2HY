using H2HY.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace H2HY.Provider
{
    /// <summary>
    /// Provider for write and save a model into a json file. T has to be serializable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileJsonProvider<T> : FileProviderBase<T> where T : IIDInterface
    {
        /// <summary>
        /// Provider for write and save a model into a json file.
        /// </summary>
        /// <param name="filename"></param>
        public FileJsonProvider(string filename) : base(filename)
        {
        }

        /// <summary>
        /// loads/deserialize a list<typeparamref name="T"/> from the given filename.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="list"></param>
        protected override void LoadModel(string filename, out List<T>? list)
        {
            if (File.Exists(filename))
            {
                FileStream stream = new(filename, FileMode.Open);
                try
                {
                    list = JsonSerializer.Deserialize<List<T>>(stream);
                }
                catch (System.Exception)
                {
                    list = new List<T>();
                    stream.Close();

                    throw;
                }

                stream.Close();
            }
            else
            {
                list = new List<T>();
            }
        }

        /// <summary>
        /// serialize a list<typeparamref name="T"/> to the given filename.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="list"></param>
        protected override void SaveModel(string filename, IEnumerable<T> list)
        {
            FileStream outputfile = File.Create(filename);
            JsonSerializer.Serialize(outputfile, list);
            outputfile.Close();
        }
    }
}