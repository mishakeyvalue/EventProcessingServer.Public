using Microsoft.AspNetCore.Mvc;
using MyEventTracker.Core;
using MyEventTracker.Server.Services;

namespace MyEventTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly EventStorage eventStorage;

        public EventController(EventStorage eventStorage)
        {
            this.eventStorage = eventStorage;
        }

        // POST api/event
        [HttpPost]
        public void Post([FromBody]EventsDto dto)
        {
            var serializer = SerializerFactory.GetSerializer<GameEvent>(dto.SerializerType);
            var events = serializer.Deserialize(dto.Data);

            eventStorage.SaveAll(events);
        }        
    }
}
