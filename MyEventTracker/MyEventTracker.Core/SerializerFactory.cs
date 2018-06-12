using MyEventTracker.Core.Serializers;
using System;

namespace MyEventTracker.Core
{
    public static class SerializerFactory
    {
        public static ISerializer<T> GetSerializer<T>(SerializerType serializerType)
        {
            switch (serializerType)
            {
                case SerializerType.Json:
                    return new JsonSerializer<T>();
                case SerializerType.Xml:
                    return new XmlSerializer<T>();
                case SerializerType.Csv:
                    return new CsvSerializer<T>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(serializerType));
            }
        }
    }

}