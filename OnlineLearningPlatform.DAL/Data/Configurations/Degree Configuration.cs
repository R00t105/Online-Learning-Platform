using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
    {
        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder.HasKey(d => d.DegreeId);

            builder.HasOne(d => d.User)
                   .WithMany() 
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Quiz)
                   .WithMany(q => q.Degrees) 
                   .HasForeignKey(d => d.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.Mark)
                   .IsRequired();

            builder.Property(d => d.AttemptDateTime)
                   .IsRequired();
        }
    }
}