using Domain.EventTypes;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;

namespace RestService.EventTypes
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public sealed class EventTypesController : ControllerBase
    {
        private readonly IEventTypesRepository eventTypesRepository;

        public EventTypesController(IEventTypesRepository eventTypesRepository) => 
            this.eventTypesRepository = eventTypesRepository;

        [HttpGet]
        public IActionResult Get()
        {
            var events = eventTypesRepository.GetAll();

            return Ok(events.ToOutput());
        }
    }
}
