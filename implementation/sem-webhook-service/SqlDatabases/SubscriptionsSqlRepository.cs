using Domain.Events;
using Domain.Subscriptions;
using RestService.Subscriptions;
using System;
using System.Collections.Generic;

namespace SqlDatabases
{
    public sealed class SubscriptionsSqlRepository : ISubscriptionsRepository
    {
        public IEnumerable<Subscription> Get()
        {
            var subscriptions = new List<Subscription>
            {
                new Subscription
                (
                    new Event
                    (
                        new Guid("c46f73fa-209d-4200-a09b-260695572451"),
                        "CatalogItemAdded"
                    ),
                    "https://eventbus.lambda-college.nl/catalogitemadded",
                    "$0m3S3cr3t!12"
                ),
                new Subscription
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

            return subscriptions;
        }
    }
}
