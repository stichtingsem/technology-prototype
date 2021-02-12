using Domain.Events;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;

namespace RestService.Events
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public sealed class EventsController : ControllerBase
    {
        private readonly IEventsRepository eventsRepository;

        public EventsController(IEventsRepository eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var events = eventsRepository.Get();

            return Ok(events);
        }
    }
}
