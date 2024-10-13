using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.Title)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(c => c.Description)
               .HasColumnType("varchar(300)")
               .IsRequired(false);
            
            builder.Property(c => c.CreationDate)
               .HasDefaultValue(DateTime.Now)
               .IsRequired(false);

            builder.HasOne<Track>(navigationExpression: c => c.Track)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TrackId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
        }
    }
}
