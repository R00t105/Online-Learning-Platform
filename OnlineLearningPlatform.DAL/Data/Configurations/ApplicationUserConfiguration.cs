using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(ap => ap.BirthDate)
                .HasColumnType("Date")
                .IsRequired(false);

            builder.Property(ap => ap.RegistrationDate)
               .HasColumnType("datetime2")
               .HasDefaultValueSql("GETDATE()")
               .IsRequired(false);
        }
    }
}               