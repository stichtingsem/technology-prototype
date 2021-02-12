using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using System;
using System.Collections.Generic;

namespace RestService.Events
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var events = new List<EventDto>
            {
                new EventDto(new Guid("c46f73fa-209d-4200-a09b-260695572451"), "CatalogItemAdded"),
                new EventDto(new Guid("145f29ad-3115-415c-8e45-c20a0b24949b"), "CatalogItemDeleted"),
                new EventDto(new Guid("701b5a97-6971-463c-9088-1e3520b3a24f"), "CatalogItemUpdated"),
            };

            return Ok(events);
        }
    }
}
