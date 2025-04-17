using AFI.Data.Sqlite.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFI.Data.Sqlite
{
    public class CustomerRegistrationDbContext : DbContext
    {
        public DbSet<CustomerRecord> CustomerRecords { get; set; }

        public CustomerRegistrationDbContext(DbContextOptions<CustomerRegistrationDbContext> options)
            : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRecord>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
