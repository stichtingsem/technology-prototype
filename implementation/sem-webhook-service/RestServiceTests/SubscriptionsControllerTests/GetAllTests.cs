using Domain.Events;
using Domain.Schools;
using Domain.Subscriptions;
using Moq;
using NUnit.Framework;
using RestService.Subscriptions;

namespace RestServiceTests.SubscriptionsControllerTests
{
    public sealed class GetAllTests
    {
        SubscriptionsController sut;

        Mock<IEventsRepository> mockEventsRepository;
        Mock<ISubscriptionsRepository> mockSubscriptionsRepository;
        Mock<ISchool> mockSchool;

        [SetUp]
        public void SetUp()
        {
            mockSubscriptionsRepository = new Mock<ISubscriptionsRepository>();
            mockEventsRepository = new Mock<IEventsRepository>();
            mockSchool = new Mock<ISchool>();

            sut = new SubscriptionsController
            (
                mockSubscriptionsRepository.Object,
                mockEventsRepository.Object,
                mockSchool.Object
            );
        }

        [Test]
        public void Default()
        {
            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
