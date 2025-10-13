using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationService.Aggregate.RegistrationAggregate;

namespace RegistrationService.Configurations
{
    public class RegistrationConfigurations : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(e => e.Id);


            // Configure column properties
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(15);

           

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255); // Assuming hashed password storage

           

            // Index on Email for fast lookup
            builder.HasIndex(e => e.Email)
                .IsUnique();


        }
    }
}
