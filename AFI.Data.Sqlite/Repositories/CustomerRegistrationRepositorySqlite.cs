using AFI.Data.Sqlite;
using AFI.Data.Sqlite.Entities;
using AFI.Domain.Registration;
using AFI.Domain.Registration.Interfaces;

namespace AFI.Data.Sqlite.Repositories
{
    public class CustomerRegistrationRepositorySqlite : ICustomerRegistrationRepository
    {
        private readonly CustomerRegistrationDbContext _dbContext;

        public CustomerRegistrationRepositorySqlite(CustomerRegistrationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Customer> RegisterCustomerAsync(Customer customer)
        {
            var entity = new CustomerRecord {
                FirstName = customer.FirstName,
                Surname = customer.Surname,
                PolicyReferenceNumber = customer.PolicyReferenceNumber,
                DateOfBirth = customer.DateOfBirth,
                Email = customer.Email };

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return Customer.CreateExisting(
                entity.Id,
                entity.FirstName,
                entity.Surname,
                entity.PolicyReferenceNumber,
                entity.DateOfBirth,
                entity.Email);
        }
    }
}
