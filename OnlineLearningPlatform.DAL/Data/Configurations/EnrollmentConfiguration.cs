using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
        
            builder.Property(e => e.ApplicationUserId)
                .IsRequired(); 

            builder.Property(e => e.CourseId)
                .IsRequired(); 

            builder.Property(e => e.EnrolmentDate)
                .IsRequired(false); 

            builder.Property(e => e.ProgressId)
                .IsRequired();

            builder.Property(e => e.ProgressState)
                .HasMaxLength(50) 
                .IsRequired(false); 

            builder.Property(e => e.CompilationDate)
                .IsRequired(false); 

            builder.Property(e => e.ProgressPercentage)
                .IsRequired();

            builder.HasKey(e => new { e.ApplicationUserId, e.CourseId });

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
