﻿using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlRepositories
{
    public sealed class EventsSqlRepository : IEventsRepository
    {
        readonly List<Event> events = new List<Event>
        {
            new Event(new Guid("c46f73fa-209d-4200-a09b-260695572451"), "CatalogItemAdded"),
            new Event(new Guid("145f29ad-3115-415c-8e45-c20a0b24949b"), "CatalogItemDeleted"),
            new Event(new Guid("701b5a97-6971-463c-9088-1e3520b3a24f"), "CatalogItemUpdated"),
        };

        public Event Get(Guid eventId) => events.SingleOrDefault(e => e.IsFor(eventId));

        public IEnumerable<Event> GetAll() => events;
    }
}