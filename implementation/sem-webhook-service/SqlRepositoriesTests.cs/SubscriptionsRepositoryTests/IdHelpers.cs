﻿using Domain.Events;
using Domain.Schools;
using System;

namespace SqlRepositoriesTests.SubscriptionsRepositoryTests
{
    public static class IdHelpers
    {
        public static SchoolId RandomSchoolId() => Guid.NewGuid();
        public static EventId RandomEventId() => Guid.NewGuid();
    }
}
