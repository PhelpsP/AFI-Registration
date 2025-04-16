using AFI.Domain.Registration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Data.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository CreateRepository<T>()
        {
            if(typeof(T) == typeof(ICustomerRegistrationRepository))
            {
                return new CustomerRegistrationRepository();
            }

            throw new InvalidOperationException(
                $"Repository of type {typeof(T).Name} not recognised.");
        }
    }
}
