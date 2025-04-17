using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Domain.Registration.Interfaces
{
    public interface ICustomerRegistrationRepository : IRepository
    {
        Task<Customer> RegisterCustomerAsync(Customer customer);
    }
}
