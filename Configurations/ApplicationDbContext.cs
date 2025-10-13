using Microsoft.EntityFrameworkCore;
using RegistrationService.Aggregate.AddressAggregate;
using RegistrationService.Aggregate.RegistrationAggregate;
using System.Reflection;

namespace RegistrationService.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
