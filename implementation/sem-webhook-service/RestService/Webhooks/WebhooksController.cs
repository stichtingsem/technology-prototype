using Domain.Events;
using Domain.Tenants;
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
        private readonly ITenant tenant;

        public WebhooksController
        (
            IWebhooksRepository webhooksRepository,
            IEventsRepository eventsRepository,
            ITenant tenant
        )
        {
            this.webhooksRepository = webhooksRepository;
            this.eventsRepository = eventsRepository;
            this.tenant = tenant;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<WebhookOutput> Get(Guid id)
        {
            var maybeWebhook = webhooksRepository.Get(id, tenant.Id);

            return maybeWebhook.Match<ActionResult<WebhookOutput>>
            (
                none: () => NotFound(),
                some: (webhook) => Ok(webhook.ToOutput(eventsRepository.GetAll()))
            );
        }

        [HttpGet]
        public ActionResult<IEnumerable<WebhookOutput>> Get()
        {
            var webhooks = webhooksRepository.GetAll(tenant.Id);

            var events = eventsRepository.GetAll();

            return Ok(webhooks.ToOutput(events));
        }

        [HttpPost]
        public ActionResult Add([FromBody] WebhookPost webhookPost)
        {
            var webhook = Webhook.Create(tenant.Id, webhookPost.EventIds, webhookPost.PostbackUrl, webhookPost.Secret);

            webhooksRepository.Add(webhook);

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] WebhookPut webhookPut)
        {
            var webhook = Webhook.Create(webhookPut.Id, tenant.Id, webhookPut.EventIds, webhookPut.PostbackUrl, webhookPut.Secret);

            webhooksRepository.Update(webhook);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(Guid id)
        {
            webhooksRepository.Delete(id, tenant.Id);

            return Ok();
        }
    }
}
