using System.Collections.Generic;

namespace MyEventTracker.Core
{
    public interface ISerializer<T>
    {
        string FileExtension { get; }

        string Serialize(IEnumerable<T> obj);
        IEnumerable<T> Deserialize(string data);
    }

    public enum SerializerType
    {
        Json,
        Xml,
        Csv
    }
}