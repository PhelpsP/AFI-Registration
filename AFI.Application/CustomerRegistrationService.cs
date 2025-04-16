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

        public Task<int> RegisterCustomerAsync()
        {
            return Task.FromResult(1);
        }
    }
}
