using AFI.Data.Repositories;
using AFI.Data.Sqlite;
using AFI.Domain.Registration.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AFI.Tests.Data
{

    [TestClass]
    public sealed class RepositoryFactoryTests
    {
        private SqliteConnection _connection;
        private CustomerRegistrationDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<CustomerRegistrationDbContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new CustomerRegistrationDbContext(options);
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public void CreateRepository_ThrowsInvalidOperationException_UnrecognisedRepository()
        {
            var target = new RepositoryFactory(_context);

            Assert.ThrowsException<InvalidOperationException>(() => target.CreateRepository<ITestRepository>());
        }

        [TestMethod]
        public void CreateRepository_ReturnsCustomerRegistrationRepository_ForICustomerRegistrationRepositoryRequest()
        {
            var target = new RepositoryFactory(_context);
            var actual = target.CreateRepository<ICustomerRegistrationRepository>();

            Assert.IsInstanceOfType(actual, typeof(CustomerRegistrationRepositorySqlite));
        }

        [TestCleanup]
        public void Cleanup() {
            _context.Dispose();
            _connection.Close();

    }
}
