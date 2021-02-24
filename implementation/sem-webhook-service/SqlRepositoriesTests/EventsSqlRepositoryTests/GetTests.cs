using Domain.Events;
using NUnit.Framework;
using System;
using System.Linq;

namespace SqlRepositoriesTests.EventsSqlRepositoryTests
{
    public class GetTests : EventsSqlRepositorySetup
    {
        [Test]
        public void ExistingEvent()
        {
            var anEvent = sut.GetAll().First();

            var result = sut.Get(anEvent.Id);

            result.Match(
                none: () => Assert.Fail(),
                some: (e) =>
                {
                    Assert.AreEqual(e.Id, anEvent.Id);
                    Assert.AreEqual(e.Name, anEvent.Name);
                });
        }

        [Test]
        public void NonExistingEvent()
        {
            EventId randomEventId = Guid.NewGuid();

            var result = sut.Get(randomEventId);

            result.Match(
                none: () => Assert.Pass(),
                some: (e) => Assert.Fail());
        }
    }
}
