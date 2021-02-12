using Domain.Schools;
using Domain.Subscriptions;
using Moq;
using NUnit.Framework;
using RestService.Subscriptions;

namespace RestServiceTests.SubscriptionsControllerTests
{
    public class GetTests
    {
        SubscriptionsController sut;
        Mock<ISubscriptionsRepository> mockSubscriptionsRepo;
        Mock<ISchool> mockSchool;

        [SetUp]
        public void SetUp()
        {
            mockSubscriptionsRepo = new Mock<ISubscriptionsRepository>();
            mockSchool = new Mock<ISchool>();

            sut = new SubscriptionsController
            (
                mockSubscriptionsRepo.Object,
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
