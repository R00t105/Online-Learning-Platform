using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.ApplicationUserId, e.CourseId });

            builder.Property(e => e.ApplicationUserId)
                .IsRequired();

            builder.Property(e => e.CourseId)
                .IsRequired();

            builder.Property(e => e.EnrollmentDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired(false);

            builder.Property(e => e.CompletionDate)
                .IsRequired(false);

            builder.Property(e => e.ProgressPercentage)
                .HasDefaultValue(0)
                .IsRequired(false);

            builder.Property(e => e.ProgressState)
                .HasDefaultValue(ProgressState.NotStarted.ToString())
                .IsRequired(false);


            builder.HasOne<ApplicationUser>(app => app.ApplicationUser)
                .WithMany(en => en.Enrollments) 
                .HasForeignKey(app => app.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Course>(co => co.Course)
               .WithMany(en => en.Enrollments)
               .HasForeignKey(co => co.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
