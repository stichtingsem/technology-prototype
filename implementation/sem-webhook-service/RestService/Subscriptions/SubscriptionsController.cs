using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
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
        [Route("{subscriptionId}")]
        public ActionResult<SubscriptionOutput> Get(Guid subscriptionId)
        {
            var subscription = subscriptionsRepository.Get(subscriptionId, school.Id);

            var events = eventsRepository.GetAll();

            return Ok(subscription.ToOutput(events));
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubscriptionOutput>> Get()
        {
            var subscriptions = subscriptionsRepository.GetAll(school.Id);

            var events = eventsRepository.GetAll();

            return Ok(subscriptions.ToOutput(events));
        }

        [HttpPost]
        public ActionResult Add([FromBody] SubscriptionAdd add)
        {
            var subscription = Subscription.Create(school.Id, add.EventIds, add.PostbackUrl, add.Secret);

            subscriptionsRepository.Add(subscription);

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] SubscriptionUpdate update)
        {
            var subscription = Subscription.Create(update.SubscriptionId, school.Id, update.EventIds, update.PostbackUrl, update.Secret);

            subscriptionsRepository.Update(subscription, school.Id);

            return Ok();
        }

        [HttpDelete]
        [Route("{subscriptionId}")]
        public ActionResult Delete(Guid subscriptionId)
        {
            subscriptionsRepository.Delete(subscriptionId, school.Id);

            return Ok();
        }
    }
}
