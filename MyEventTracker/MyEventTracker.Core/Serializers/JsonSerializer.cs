using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyEventTracker.Core.Serializers
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public string FileExtension { get; } = ".json";

        public IEnumerable<T> Deserialize(string data) => JsonConvert.DeserializeObject<IEnumerable<T>>(data);

        public string Serialize(IEnumerable<T> obj) => JsonConvert.SerializeObject(obj);
    }
}
