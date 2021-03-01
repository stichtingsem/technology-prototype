using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestService.Notifications
{
   [ApiController]
   [Route("[controller]")]
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

   }
}
