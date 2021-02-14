using Domain.Events;
using Moq;
using NUnit.Framework;
using RestService.Events;

namespace RestServiceTests.EventsControllerTests
{
    public sealed class GetAllTests
    {
        EventsController sut;
        Mock<IEventsRepository> mockEventsRepository;

        [SetUp]
        public void SetUp()
        {
            mockEventsRepository = new Mock<IEventsRepository>();
            sut = new EventsController(mockEventsRepository.Object);
        }

        [Test]
        public void Default()
        {
            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
