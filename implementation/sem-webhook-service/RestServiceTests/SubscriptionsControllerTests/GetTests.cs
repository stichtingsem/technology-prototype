using NUnit.Framework;
using RestService.Subscriptions;

namespace RestServiceTests.SubscriptionsControllerTests
{
    public class GetTests
    {
        [Test]
        public void Default()
        {
            var sut = new SubscriptionsController();

            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
