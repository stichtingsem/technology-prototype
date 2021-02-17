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
        [Route("{id}")]
        public ActionResult<WebhookOutput> Get(Guid id)
        {
            var webhook = webhooksRepository.Get(id, school.Id);

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
        public ActionResult Add([FromBody] WebhookPost webhookPost)
        {
            var webhook = Webhook.Create(school.Id, webhookPost.EventIds, webhookPost.PostbackUrl, webhookPost.Secret);

            webhooksRepository.Add(webhook);

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] WebhookPut webhookPut)
        {
            var webhook = Webhook.Create(webhookPut.Id, school.Id, webhookPut.EventIds, webhookPut.PostbackUrl, webhookPut.Secret);

            webhooksRepository.Update(webhook);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(Guid id)
        {
            webhooksRepository.Delete(id, school.Id);

            return Ok();
        }
    }
}
