using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(co => co.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(co => co.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(co => co.Title)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(co => co.VideoUrl)
                .IsRequired(false);

            builder.Property(co => co.IsRead)
                .HasDefaultValue(false);

            builder.HasOne<Course>(co => co.Course)
                .WithMany(c => c.Contents)
                .HasForeignKey(co => co.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
