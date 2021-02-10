using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Webhook> Get()
        {
            return new List<Webhook>();
        }
    }
}
