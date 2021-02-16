using Domain.Events;
using Domain.Schools;
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
        Mock<ISchool> mockSchool;

        [SetUp]
        public void SetUp()
        {
            mockWebhooksRepository = new Mock<IWebhooksRepository>();
            mockEventsRepository = new Mock<IEventsRepository>();
            mockSchool = new Mock<ISchool>();

            sut = new WebhooksController
            (
                mockWebhooksRepository.Object,
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
