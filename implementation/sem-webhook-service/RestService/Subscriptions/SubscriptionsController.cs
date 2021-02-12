using Domain.Schools;
using Domain.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using System;
using System.Runtime.InteropServices;

namespace RestService.Subscriptions
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public sealed class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsRepository subscriptionsRepository;
        private readonly ISchool school;

        public SubscriptionsController
        (
            ISubscriptionsRepository subscriptionsRepository,
            ISchool school
        )
        {
            this.subscriptionsRepository = subscriptionsRepository;
            this.school = school;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var subscriptions = subscriptionsRepository.Get(school);

            return Ok(subscriptions);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SubscriptionInput input)
        {
            var subscription = new Subscription(school.Id, input.EventId, input.PostbackUrl, input.Secret);

            subscriptionsRepository.Add(subscription);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] SubscriptionInput input)
        {
            var subscription = new Subscription(school.Id, input.EventId, input.PostbackUrl, input.Secret);

            subscriptionsRepository.Add(subscription);

            return Ok();
        }

        [HttpGet]
        [Route("{eventId}")]
        public IActionResult Get(Guid eventId)
        {
            var subscription = subscriptionsRepository.Get(school, eventId);

            return Ok(subscription);
        }

        [HttpDelete]
        [Route("{eventId}")]
        public IActionResult Delete(Guid eventId)
        {
            subscriptionsRepository.Delete(school, eventId);

            return Ok();
        }
    }
}
