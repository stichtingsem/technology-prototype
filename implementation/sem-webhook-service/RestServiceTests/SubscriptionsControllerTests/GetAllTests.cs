using Domain.Events;
using Domain.Tenants;
using Domain.Webhooks;
using Moq;
using NUnit.Framework;
using RestService.Webhooks;

namespace RestServiceTests.WebhooksControllerTests
{
    public sealed class GetAllTests
    {
        WebhooksController sut;

        Mock<IEventsRepository> mockEventsRepository;
        Mock<IWebhooksRepository> mockWebhooksRepository;
        Mock<ITenant> mockTenant;

        [SetUp]
        public void SetUp()
        {
            mockWebhooksRepository = new Mock<IWebhooksRepository>();
            mockEventsRepository = new Mock<IEventsRepository>();
            mockTenant = new Mock<ITenant>();

            sut = new WebhooksController
            (
                mockWebhooksRepository.Object,
                mockEventsRepository.Object,
                mockTenant.Object
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
