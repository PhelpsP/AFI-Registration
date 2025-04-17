using AFI.Data.Repositories;
using AFI.Data.Sqlite;
using AFI.Domain.Registration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AFI.Tests.Data.Sqlite
{
    [TestClass]
    public sealed class CustomerRegistrationRepositorySqliteTests
    {
        private SqliteConnection _connection;
        private CustomerRegistrationDbContext _context;
        private CustomerRegistrationRepositorySqlite _repository;

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

            _repository = new CustomerRegistrationRepositorySqlite(_context);
        }

        [TestMethod]
        public async Task RegisterCustomerAsync_ShouldAutoIncrementIdAndAddCustomerRecord()
        {
            int expectedFirstCustomerId = 1;
            int expectedSecondCustomerId = 2;

            var customerOne = Customer.CreateNew("Test", "Tester", "AB-123456", null, "Test@Test.com");
            var customerTwo = Customer.CreateNew("Test", "Testor", "ZX-654321", null, "Testor@Test.co.uk");

            var customerRecordOne = await _repository.RegisterCustomerAsync(customerOne);

            Assert.AreEqual(expectedFirstCustomerId, customerRecordOne.CustomerId);
            
            var customerRecordTwo = await _repository.RegisterCustomerAsync(customerTwo);
            Assert.AreEqual(expectedSecondCustomerId, customerRecordTwo.CustomerId);
        }

        [TestMethod]
        public async Task RegisterCustomerAsync_ShouldAddCustomerRecord()
        {
            int expectedCustomerId = 1;
            var customer = Customer.CreateNew("Test", "Tester", "AB-123456", null, "Test@Test.com");

            var customerRecord = await _repository.RegisterCustomerAsync(customer);

            Assert.AreEqual(expectedCustomerId, customerRecord.CustomerId);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
            _connection.Close();
        }
    }
}
