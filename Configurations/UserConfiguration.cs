using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationService.Aggregate;
using System.Reflection;

namespace RegistrationService.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(m => m.Name, b =>
            {
                b.WithOwner();

                b.Property(m => m.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired();

                b.Property(m => m.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(false);

            });

            builder.Property(m => m.MobileNumber)
                .HasConversion(m => m == null || m == MobileNumber.Empty ? null : m.Value, a => a == null ? null : (MobileNumber)a)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired(false);

            builder.HasIndex(x => x.MobileNumber)
                .IsUnique();

            builder.Property(m => m.MobileNumberWithoutCountryCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired(false);

            builder.HasIndex(m => m.MobileNumberWithoutCountryCode)
                .IsUnique(false);

            builder.Property(m => m.EmailAddress)
                .HasConversion(m => m == null || m == EmailAddress.Empty ? null : m.Value, a => a == null ? null : (EmailAddress)a)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(m => m.NormalizedEmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired(false);

            builder.HasIndex(m => m.NormalizedEmailAddress)
                .IsUnique();

            builder.Property(m => m.Locale)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsRequired();

            //builder.Property(m => m.TimeZone)
            //    .HasConversion(m => m.Id, a => TimeZoneInfo.FindSystemTimeZoneById(a) )
            //    .HasMaxLength(200)
            //    .IsUnicode(false)
            //    .IsRequired();



            //builder.HasMany(m => m.Logins)
            //    .WithOne()
            //    .HasForeignKey(m => m.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(m => m.Roles)
            //    .WithOne()
            //    .HasForeignKey(m => m.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.Property<string>("_passwordHash")
                .HasColumnName("PasswordHash")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(m => m.PictureUrl)
                .IsUnicode(false)
                .IsRequired(false);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.ReferrerUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Gender>()
                .WithMany()
                .HasForeignKey(m => m.GenderId)
                .IsRequired(false);


            builder.Property(m => m.DateOfBirth)
                .HasColumnName("DateOfBirth")
               .HasColumnType("date")

               .HasConversion(x => x.Value, x => DateOfBirth.Create(x.Value).Value)
               .IsRequired(false);

            builder.OwnsOne(m => m.Location, b =>
            {
                b.Property(d => d.Latitude)
                    .HasColumnType("decimal(18,15)");
                b.Property(d => d.Longitude)
                    .HasColumnType("decimal(18,15)");

                b.OwnsOne(c => c.Area, d =>
                {
                    d.Property(e => e.Name).HasMaxLength(400).IsRequired(false);
                });
            });

            builder.OwnsOne(m => m.Creator, b =>
            {
                b.Property(c => c.Id);
                b.Property(c => c.Name).HasMaxLength(100);
            });
        }
    }
}
