using Domain.Events;
using Domain.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using SqlDatabases;
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

        public SubscriptionsController(ISubscriptionsRepository subscriptionsRepository)
        {
            this.subscriptionsRepository = subscriptionsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // A way to get the school id of the user:
            // var schoolId = User.Claims.ToList().Single(claim => claim.Type == "actOrgId");
            var subscriptions = subscriptionsRepository.Get(); 

            return Ok(subscriptions);
        }
    }
}
