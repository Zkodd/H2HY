using H2HY.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace H2HY.Provider
{
    /// <summary>
    /// Provider for write and save a model into xml a file. T have to be serialiseable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileXmlProvider<T> : FileProviderBase<T> where T : IIDInterface
    {
        /// <summary>
        /// Create a xml provider using the given filename.
        /// </summary>
        /// <param name="filename"></param>
        public FileXmlProvider(string filename) : base(filename)
        {
        }

        /// <summary>
        /// loads/deserialize a list<typeparamref name="T"/>
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="list"></param>
        protected override void LoadModel(string filename, out List<T>? list)
        {
            if (File.Exists(filename))
            {
                //An exception is thrown but handled by the XmlSerializer,
                //so if you just ignore it everything should continue fine.
                XmlSerializer reader = new XmlSerializer(typeof(List<T>));
                StreamReader file = new StreamReader(filename);
                list = reader.Deserialize(file) as List<T>;
                file.Close();
            }
            else
            {
                list = new List<T>();
            }
        }

        /// <summary>
        /// serializes a list<typeparamref name="T"/>
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="list"></param>
        protected override void SaveModel(string filename, IEnumerable<T> list)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<T>));
            FileStream outputfile = File.Create(filename);
            writer.Serialize(outputfile, list);
            outputfile.Close();
        }
    }
}