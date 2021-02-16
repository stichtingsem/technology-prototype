using Domain.Events;
using Domain.Schools;
using Domain.Webhooks;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using System;
using System.Collections.Generic;

namespace RestService.Webhooks
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public sealed class WebhooksController : ControllerBase
    {
        private readonly IWebhooksRepository webhooksRepository;
        private readonly IEventsRepository eventsRepository;
        private readonly ISchool school;

        public WebhooksController
        (
            IWebhooksRepository webhooksRepository,
            IEventsRepository eventsRepository,
            ISchool school
        )
        {
            this.webhooksRepository = webhooksRepository;
            this.eventsRepository = eventsRepository;
            this.school = school;
        }

        [HttpGet]
        [Route("{webhookId}")]
        public ActionResult<WebhookOutput> Get(Guid webhookId)
        {
            var webhook = webhooksRepository.Get(webhookId, school.Id);

            var events = eventsRepository.GetAll();

            return Ok(webhook.ToOutput(events));
        }

        [HttpGet]
        public ActionResult<IEnumerable<WebhookOutput>> Get()
        {
            var webhooks = webhooksRepository.GetAll(school.Id);

            var events = eventsRepository.GetAll();

            return Ok(webhooks.ToOutput(events));
        }

        [HttpPost]
        public ActionResult Add([FromBody] WebhookAdd add)
        {
            var webhook = Webhook.Create(school.Id, add.EventIds, add.PostbackUrl, add.Secret);

            webhooksRepository.Add(webhook);

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] WebhookUpdate update)
        {
            var webhook = Webhook.Create(update.WebhookId, school.Id, update.EventIds, update.PostbackUrl, update.Secret);

            webhooksRepository.Update(webhook, school.Id);

            return Ok();
        }

        [HttpDelete]
        [Route("{webhookId}")]
        public ActionResult Delete(Guid webhookId)
        {
            webhooksRepository.Delete(webhookId, school.Id);

            return Ok();
        }
    }
}
