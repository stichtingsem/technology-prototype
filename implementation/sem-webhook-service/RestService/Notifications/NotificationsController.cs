using Abp.Dependency;
using Abp.Webhooks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestService.Notifications
{
    [ApiController]
    [Route("test/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private static List<WebhookPayload> Notifications = new List<WebhookPayload>();

        [HttpGet]
        public ActionResult<IEnumerable<WebhookPayload>> Get()
        {
            return Ok(Notifications);
        }

        [HttpPost]
        public ActionResult Add([FromBody] WebhookPayload data)
        {
            Notifications.Add(data);

            return Ok();
        }


        [HttpGet]
        [Route("fire")]
        public ActionResult Fire()
        {
            var publisher = IocManager.Instance.Resolve<IWebhookPublisher>();
            publisher.PublishAsync("catalog.updated", "demo time");

            return Ok();
        }

        [HttpGet]
        [Route("fire/{tentantID}")]
        public ActionResult Fire(int tentantID)
        {
            var publisher = IocManager.Instance.Resolve<IWebhookPublisher>();
            publisher.PublishAsync("catalog.updated", "demo time", tentantID);

            return Ok();
        }
    }
}
