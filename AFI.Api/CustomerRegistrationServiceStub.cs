using AFI.Api.Models;
using AFI.Domain.Registration.Interfaces;

namespace AFI.Api
{
    public class CustomerRegistrationServiceStub : ICustomerRegistrationService
    {
        public Task<int> RegisterCustomerAsync()
        {
            var response = new RegistrationResponse { CustomerId = 1 };

            return Task.FromResult(response.CustomerId);
        }
    }
}
