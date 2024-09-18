using Microsoft.EntityFrameworkCore;
using RegistrationService.Aggregate;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
