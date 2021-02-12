using Domain.Subscriptions;
using Moq;
using NUnit.Framework;
using RestService.Subscriptions;
using System.Security.Cryptography.Xml;

namespace RestServiceTests.SubscriptionsControllerTests
{
    public class GetTests
    {
        SubscriptionsController sut;
        Mock<ISubscriptionsRepository> mockSubscriptionsRepo;

        [SetUp]
        public void SetUp()
        {
            mockSubscriptionsRepo = new Mock<ISubscriptionsRepository>();
            sut = new SubscriptionsController(mockSubscriptionsRepo.Object);
        }

        [Test]
        public void Default()
        {
            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
