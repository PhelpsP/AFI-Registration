using AFI.Data.Repositories;
using AFI.Domain.Registration.Interfaces;

namespace AFI.Tests.Data
{
    public interface ITestRepository : IRepository
    {
    }

    [TestClass]
    public sealed class RepositoryFactoryTests
    {
        [TestMethod]
        public void CreateRepository_ThrowsInvalidOperationException_UnrecognisedRepository()
        {
            var target = new RepositoryFactory();

            Assert.ThrowsException<InvalidOperationException>(() => target.CreateRepository<ITestRepository>());
        }

        [TestMethod]
        public void CreateRepository_ReturnsCustomerRegistrationRepository_ForICustomerRegistrationRepositoryRequest()
        {
            var target = new RepositoryFactory();
            var actual = target.CreateRepository<ICustomerRegistrationRepository>();

            Assert.IsInstanceOfType(actual, typeof(CustomerRegistrationRepository));
        }
    }
}
