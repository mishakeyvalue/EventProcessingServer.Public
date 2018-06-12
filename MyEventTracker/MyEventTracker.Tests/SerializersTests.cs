using FsCheck;
using FsCheck.Xunit;
using MyEventTracker.Core;
using MyEventTracker.Core.Serializers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyEventTracker.Tests
{
    public class SerializersTests
    {
        private GameEvent TestEvent = new GameEvent()
        {
            Id = 42,
            Parameters = new Dictionary<string, string>()
                    {
                        { "key1","val1" },
                        { "key2","val2" },
                        { "key3","val3" },
                        { "key4","val4" },
                        { "key5","val5" }
                    }
        };

        [Fact]
        public bool PropSerializationIdempotency()
        {
            var events = new List<GameEvent> { TestEvent, TestEvent };
            var serializers = new ISerializer<GameEvent>[]
            {
                new JsonSerializer<GameEvent>(),
                new XmlSerializer<GameEvent>(),
                new CsvSerializer<GameEvent>()
            };

            return serializers.Select(s => SerializationIdempotency(s, events)).All(p => p);
        }

        private static bool SerializationIdempotency(ISerializer<GameEvent> serializer, List<GameEvent> events)
        {
            string appliedOnce = serializer.Serialize(events);
            string appliedTwice = serializer.Serialize(serializer.Deserialize(serializer.Serialize(events)));
            return appliedTwice == appliedOnce;
        }
    }
}
