using NUnit.Framework;
using RestService.Events;

namespace RestServiceTests.EventsControllerTests
{
    public class GetTests
    {
        [Test]
        public void Default()
        {
            var sut = new EventsController();

            var result = sut.Get();

            Assert.IsNotNull(result);
        }
    }
}
