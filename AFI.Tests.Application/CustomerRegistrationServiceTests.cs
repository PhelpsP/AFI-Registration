using AFI.Application;
using AFI.Domain.Registration;
using AFI.Domain.Registration.Interfaces;
using Moq;

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

        [TestMethod]
        public async Task RegisterCustomerAsync_ThrowsExceptionForNullRepository()
        {
            var mockRepoFactory = new Mock<IRepositoryFactory>();
            mockRepoFactory.Setup(x => x.CreateRepository<ICustomerRegistrationRepository>())
                .Returns((ICustomerRegistrationRepository)null);

            var target = new CustomerRegistrationService(mockRepoFactory.Object);

            await Assert.ThrowsExceptionAsync<NullReferenceException>(
                () => target.RegisterCustomerAsync(new CustomerDetailsDto()));
        }

        [TestMethod]
        public async Task RegisterCustomerAsync_ReturnsCustomerIdUponRegistering()
        {
            int customerId = 9;
            var customerDto = new CustomerDetailsDto
            {
                PolicyholderFirstName = "Test",
                PolicyholderSurname = "Tester",
                PolicyReferenceNumber = "AA-123456",
                PolicyholderEmail = "Test@Testing.co.uk"
            };

            var mockRepository = new Mock<ICustomerRegistrationRepository>();
            mockRepository.Setup(x => x.RegisterCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync(
                Customer.CreateExisting(
                    customerId,
                    customerDto.PolicyholderFirstName,
                    customerDto.PolicyholderSurname,
                    customerDto.PolicyReferenceNumber,
                    customerDto.PolicyholderDOB,
                    customerDto.PolicyholderEmail));


            var mockRepoFactory = new Mock<IRepositoryFactory>();
            mockRepoFactory.Setup(x => x.CreateRepository<ICustomerRegistrationRepository>())
                .Returns(mockRepository.Object);

            var target = new CustomerRegistrationService(mockRepoFactory.Object);
            var actual = await target.RegisterCustomerAsync(customerDto);

            Assert.AreEqual(customerId, actual);
        }
    }
}
