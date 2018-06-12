using System;
using System.Collections.Generic;
using System.Linq;
using YAXLib;

namespace MyEventTracker.Core.Serializers
{
    public class XmlSerializer<T> : ISerializer<T>
    {
        public string FileExtension { get; } = ".xml";

        private YAXSerializer _serializer = new YAXSerializer(typeof(IEnumerable<T>));

        public IEnumerable<T> Deserialize(string data)
        {
            var result = ((IEnumerable<object>)_serializer.Deserialize(data)).Cast<T>();
            return result;               
        }

        public string Serialize(IEnumerable<T> obj)
        {
            var result = _serializer.Serialize(obj);
            return result;
        }
    }
}
