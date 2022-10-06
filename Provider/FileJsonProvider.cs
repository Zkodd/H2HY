using H2HY.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace H2HY.Provider
{

    /// <summary>
    /// Provider for write and save a model into a json file. T have to be serialiseable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileJsonProvider<T> : FileProviderBase<T> where T : IIDInterface
    {
        public FileJsonProvider(string filename) : base(filename)
        {
        }

        protected override void LoadModel(string filename, out List<T>? list)
        {
            if (File.Exists(filename))
            {
                FileStream stream = new(filename, FileMode.Open);
                list = JsonSerializer.Deserialize<List<T>>(stream);
                stream.Close();
            }
            else
            {
                list = new List<T>();
            }
        }

        protected override void SaveModel(string filename, IEnumerable<T> list)
        {
            FileStream outputfile = File.Create(filename);
            JsonSerializer.Serialize(outputfile, list);
            outputfile.Close();
        }
    }
}