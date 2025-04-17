using AFI.Data.Sqlite;
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
        private readonly CustomerRegistrationDbContext _dbContext;

        public RepositoryFactory(CustomerRegistrationDbContext dbContext)
        {
            _dbContext = dbContext 
                ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public T? CreateRepository<T>() where T : class
        {
            if(typeof(T) == typeof(ICustomerRegistrationRepository))
            {
                return new CustomerRegistrationRepositorySqlite(_dbContext) as T;
            }

            throw new InvalidOperationException(
                $"Repository of type {typeof(T).Name} not recognised.");
        }
    }
}
