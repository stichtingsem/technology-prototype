﻿using NUnit.Framework;
using SqlRepositories.EventTypes;

namespace SqlRepositoriesTests.EventsSqlRepositoryTests
{
    public abstract class EventTypesSqlRepositorySetup
    {
        protected EventTypesSqlRepository sut;

        private readonly RepositoryFactory factory = new RepositoryFactory();

        [SetUp]
        public void SetUp()
        {
            sut = factory.EventsTypesSqlRepository;
        }
    }
}
