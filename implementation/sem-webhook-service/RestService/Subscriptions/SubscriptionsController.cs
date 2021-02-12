using Microsoft.AspNetCore.Mvc;
using RestService.Authorization;
using System;
using System.Collections.Generic;

namespace RestService.Subscriptions
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeFromConfig]
    public class SubscriptionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var subscriptions = new List<SubscriptionDto>
            {
                new SubscriptionDto
                (
                    new Guid("c46f73fa-209d-4200-a09b-260695572451"), 
                    "https://eventbus.lambda-college.nl/catalogitemadded", 
                    "$0m3S3cr3t!12"
                ),
                new SubscriptionDto
                (
                    new Guid("145f29ad-3115-415c-8e45-c20a0b24949b"),
                    "https://eventbus.lambda-college.nl/catalogitemdeleted",
                    "$0m3S3cr3t!34"
                ),
            };

            return Ok(subscriptions);
        }
    }
}
