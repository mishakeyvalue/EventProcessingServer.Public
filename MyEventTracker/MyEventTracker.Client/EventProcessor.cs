using MyEventTracker.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyEventTracker.Client
{
    /// <summary>
    /// Class for sending game events to the server
    /// </summary>
    public class EventProcessor
    {
        public Uri ServerUri { get; set; }

        private readonly ISerializer<GameEvent> serializer;
        private readonly SerializerType serializerType;

        /// <summary>
        /// Creates new instance of EventProcessor with the given server URI and serializer for events.
        /// </summary>
        /// <param name="serverUri">URI of the server</param>
        /// <param name="serializer">Serializer</param>
        public EventProcessor(Uri serverUri, SerializerType serializerType)
        {
            ServerUri = serverUri;
            this.serializerType = serializerType;
            this.serializer = SerializerFactory.GetSerializer<GameEvent>(serializerType);
        }

        private const int BufferSize = 42;

        private List<GameEvent> inMemroyBuffer = new List<GameEvent>();

        /// <summary>
        /// Saves given event and sends to the server
        /// </summary>
        /// <param name="event">Event to store</param>
        public void Log(GameEvent @event)
        {
            inMemroyBuffer.Add(@event);

            if (inMemroyBuffer.Count > BufferSize)
                FlushBuffer();
        }

        private void FlushBuffer()
        {
            var events = serializer.Serialize(this.inMemroyBuffer);
            var dto = new EventsDto() { SerializerType = serializerType, Data = events };
            inMemroyBuffer.Clear();
            SendToServer(dto);
        }

        private void SendToServer(EventsDto events)
        {
            using (HttpClient client = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(events);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                client.PostAsync(ServerUri, content).Wait();
            }
        }
    }
}
