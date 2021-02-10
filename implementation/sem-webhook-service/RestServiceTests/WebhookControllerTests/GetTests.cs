using NUnit.Framework;
using RestService.Webhooks;

namespace RestServiceTests.WebhookControllerTests
{
    public class GetTests
    {
        [Test]
        public void EmptyResult()
        {
            var sut = new WebhookController();

            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
