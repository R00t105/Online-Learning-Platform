using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
               .ValueGeneratedOnAdd()
               .UseIdentityColumn(1, 1);

            builder.Property(t => t.Name)
               .HasColumnType("varchar(25)")
               .IsRequired();

            builder.Property(t => t.Description)
               .HasColumnType("varchar(100)")
               .IsRequired(false);

            builder.Property(t => t.CreationDate)
               .HasDefaultValue(DateTime.Now)
               .IsRequired(false);
        }
    }
}
