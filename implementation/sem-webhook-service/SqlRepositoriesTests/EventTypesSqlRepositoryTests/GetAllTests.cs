using NUnit.Framework;
using System.Linq;

namespace SqlRepositoriesTests.EventsSqlRepositoryTests
{
    public class GetAllTests : EventTypesSqlRepositorySetup
    {
        [Test]
        public void EventsExist()
        {
            var result = sut.GetAll();

            Assert.IsTrue(result.Any());
        }
    }
}
