using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using RestService.Events;
using System;
using System.Collections.Generic;

namespace RestService.Subscriptions
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public sealed class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsRepository subscriptionsRepository;
        private readonly IEventsRepository eventsRepository;
        private readonly ISchool school;

        public SubscriptionsController
        (
            ISubscriptionsRepository subscriptionsRepository,
            IEventsRepository eventsRepository,
            ISchool school
        )
        {
            this.subscriptionsRepository = subscriptionsRepository;
            this.eventsRepository = eventsRepository;
            this.school = school;
        }

        [HttpGet]
        [Route("{eventId}")]
        public ActionResult<SubscriptionOutput> Get(Guid eventId)
        {
            var eventOutput = eventsRepository.Get(eventId).ToOutput();
            var subscriptionOutput = subscriptionsRepository.Get(school.Id, eventId).ToOutput(eventOutput);

            return Ok(subscriptionOutput);
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubscriptionOutput>> Get()
        {
            var events = eventsRepository.GetAll();
            var subscriptionsOutput = subscriptionsRepository.GetAll(school.Id).ToOutput(events);

            return Ok(subscriptionsOutput);
        }

        [HttpPost]
        public ActionResult Add([FromBody] SubscriptionInput input)
        {
            var subscription = new Subscription(school.Id, input.EventId, input.PostbackUrl, input.Secret);

            subscriptionsRepository.AddOrUpdate(subscription);

            return Ok();
        }

        [HttpDelete]
        [Route("{eventId}")]
        public ActionResult Delete(Guid eventId)
        {
            subscriptionsRepository.Delete(school.Id, eventId);

            return Ok();
        }
    }
}
