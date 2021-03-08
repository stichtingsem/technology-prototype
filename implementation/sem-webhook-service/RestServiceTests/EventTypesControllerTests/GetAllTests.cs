using Domain.EventTypes;
using Moq;
using NUnit.Framework;
using RestService.EventTypes;

namespace RestServiceTests.EventTypesControllerTests
{
    public sealed class GetAllTests
    {
        EventTypesController sut;
        Mock<IEventTypesRepository> mockEventTypesRepository;

        [SetUp]
        public void SetUp()
        {
            mockEventTypesRepository = new Mock<IEventTypesRepository>();
            sut = new EventTypesController(mockEventTypesRepository.Object);
        }

        [Test]
        public void Default()
        {
            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
