using Domain.Events;
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
        [HttpGet]
        public IActionResult Get()
        {
            // A way to get the school id of the user:
            // var schoolId = User.Claims.ToList().Single(claim => claim.Type == "actOrgId");

            var subscriptions = new List<SubscriptionDto>
            {
                new SubscriptionDto
                (
                    new Event
                    (
                        new Guid("c46f73fa-209d-4200-a09b-260695572451"),
                        "CatalogItemAdded"
                    ), 
                    "https://eventbus.lambda-college.nl/catalogitemadded", 
                    "$0m3S3cr3t!12"
                ),
                new SubscriptionDto
                (
                    new Event
                    (
                        new Guid("145f29ad-3115-415c-8e45-c20a0b24949b"),
                        "CatalogItemDeleted"
                    ),
                    "https://eventbus.lambda-college.nl/catalogitemdeleted",
                    "$0m3S3cr3t!34"
                ),
            };

            return Ok(subscriptions);
        }
    }
}
