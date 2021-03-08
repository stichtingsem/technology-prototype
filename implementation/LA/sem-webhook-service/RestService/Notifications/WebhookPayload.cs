using System;

namespace RestService.Notifications
{
   public class WebhookPayload
   {
      public WebhookPayload() { }

      public string Id { get; set; }

      public string Event { get; set; }

      public int Attempt { get; set; }

      public dynamic Data { get; set; }

      public DateTime CreationTimeUtc { get; set; }
   }
}
