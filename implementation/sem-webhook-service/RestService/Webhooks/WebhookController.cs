using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public class WebhookController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Webhook>());
        }
    }
}
