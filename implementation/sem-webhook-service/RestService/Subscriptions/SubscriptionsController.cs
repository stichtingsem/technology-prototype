using Domain.Schools;
using Domain.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;

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
    }
}
