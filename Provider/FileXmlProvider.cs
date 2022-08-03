using H2HY.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace H2HY.Provider
{
    /// <summary>
    /// Provider for write and save model into xml files.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileXmlProvider<T> : FileProviderBase<T> where T : IIDInterface
    {
        public FileXmlProvider(string filename) : base(filename)
        {
        }

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

        protected override void SaveModel(string filename, List<T> list)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<T>));
            FileStream outputfile = File.Create(filename);
            writer.Serialize(outputfile, list);
            outputfile.Close();
        }
    }
}