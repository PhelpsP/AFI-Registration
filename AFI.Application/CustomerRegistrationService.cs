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

        public async Task<int> RegisterCustomerAsync(CustomerDetailsDto dto)
        {
            var repository = _repositoryFactory.CreateRepository<ICustomerRegistrationRepository>();

            if (repository == null)
            {
                throw new NullReferenceException(nameof(repository));
            }

            var customer = Customer.CreateNew(
                dto.PolicyholderFirstName,
                dto.PolicyholderSurname,
                dto.PolicyReferenceNumber,
                dto.PolicyholderDOB,
                dto.PolicyholderEmail);

            var customerRecord = await repository.RegisterCustomerAsync(customer);

            return customerRecord.CustomerId;
        }
    }
}
