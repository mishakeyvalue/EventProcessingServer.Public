using MyEventTracker.Client;
using MyEventTracker.Core;
using MyEventTracker.Core.Serializers;
using System;
using System.Collections.Generic;

namespace MyEventTracker
{
    class Program
    {
        private static GameEvent TestEvent(int id)
        {
            return new GameEvent()
            {
                Id = id,
                Parameters = new Dictionary<string, string>()
                    {
                        { "key1","val1" },
                        { "key2","val2" },
                        { "key3","val3" },
                        { "key4","val4" },
                        { "key5","val5" }
                    }
            };
        }




        static void Main(string[] args)
        {
            const int TestEventsCount = 43;
            Uri serverUri = new Uri("http://localhost:7850/api/event");
            var eventProcessor = new EventProcessor(serverUri, SerializerType.Xml);

            for (int i = 0; i < TestEventsCount; i++)
            {
                var @event = TestEvent(i);
                eventProcessor.Log(@event);
            }
        }
    }
}
