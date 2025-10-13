using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationService.Aggregate;

namespace RegistrationService.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever();

            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.HasData(Enumeration.GetAll<Gender>());
        }
    }
}
