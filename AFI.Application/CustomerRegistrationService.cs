using AFI.Domain.Registration;
using AFI.Domain.Registration.Interfaces;

namespace AFI.Application
{
    public class CustomerRegistrationService : ICustomerRegistrationService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CustomerRegistrationService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory
                ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public Task<int> RegisterCustomerAsync(CustomerDetailsDto dto)
        {
            var repository = _repositoryFactory.CreateRepository<ICustomerRegistrationRepository>();

            var customer = Customer.CreateNew(
                dto.PolicyholderFirstName,
                dto.PolicyholderSurname,
                dto.PolicyReferenceNumber,
                dto.PolicyholderDOB,
                dto.PolicyholderEmail);

            customer.Register((ICustomerRegistrationRepository)repository);

            return Task.FromResult(customer.CustomerId);
        }
    }
}
