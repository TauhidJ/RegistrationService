using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationService.Aggregate.AddressAggregate;

namespace RegistrationService.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id );

            builder.Property(e => e.UserId).IsRequired();

            //builder.HasKey(e => e.UserId);
            builder.Property(m => m.UserId).ValueGeneratedNever();

            builder.Property(d => d.Latitude).HasColumnType("decimal(18,15)");
            builder.Property(d => d.Longitude).HasColumnType("decimal(18,15)");

        }
    }
}
