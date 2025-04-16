using AFI.Application;

namespace AFI.Tests.Application
{
    [TestClass]
    public sealed class CustomerRegistrationServiceTests
    {
        [TestMethod]
        public void Constructor_ThrowsArgumentNullException_NullRepositoryFactory()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new CustomerRegistrationService(null));
        }
    }
}
