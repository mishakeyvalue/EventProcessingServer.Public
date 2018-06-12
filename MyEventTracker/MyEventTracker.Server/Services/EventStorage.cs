using MyEventTracker.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyEventTracker.Server.Services
{
    public class EventStorage
    {
        private ISerializer<GameEvent> serializer;
        private readonly string logFolderPath;


        public EventStorage(string logFolderPath, SerializerType serializerType)
        {
            this.serializer = SerializerFactory.GetSerializer<GameEvent>(serializerType);
            this.logFolderPath = logFolderPath;
        }

        private const int MemoryBufferSize = 42;
        private List<GameEvent> inMemoryBuffer = new List<GameEvent>();

        public void SaveAll(IEnumerable<GameEvent> events)
        {
            foreach (var @event in events)
            {
                Save(@event);
            }
        }

        public void Save(GameEvent @event)
        {
            inMemoryBuffer.Add(@event);
            if (MemoryBufferSize < inMemoryBuffer.Count)
                FlushToDisk();
        }

        private void FlushToDisk()
        {
            string filePath = Path.Combine(logFolderPath, "game-events" + serializer.FileExtension);
            string data = serializer.Serialize(inMemoryBuffer);
            File.AppendAllText(filePath, data);
            inMemoryBuffer.Clear();
        }
    }
}
