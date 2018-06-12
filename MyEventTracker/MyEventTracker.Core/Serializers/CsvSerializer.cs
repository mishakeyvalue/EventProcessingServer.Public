using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyEventTracker.Core.Serializers
{
    public class CsvSerializer<T> : ISerializer<T>
    {
        public string FileExtension { get; } = ".csv";

        public IEnumerable<T> Deserialize(string data)
        {
            var result = CsvSerializer.DeserializeFromString<IEnumerable<T>>(data);
            return result;
        }

        public string Serialize(IEnumerable<T> obj)
        {
            var result = CsvSerializer.SerializeToCsv<T>(obj);
            return result;
        }
    }
}
