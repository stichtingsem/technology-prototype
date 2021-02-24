using NUnit.Framework;
using SqlRepositories.Events;

namespace SqlRepositoriesTests.EventsSqlRepositoryTests
{
    public abstract class EventsSqlRepositorySetup
    {
        protected EventsSqlRepository sut;

        private readonly RepositoryFactory factory = new RepositoryFactory();

        [SetUp]
        public void SetUp()
        {
            sut = factory.EventsSqlRepository;
        }
    }
}
