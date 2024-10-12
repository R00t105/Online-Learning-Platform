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
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(r => r.Mark)
                .IsRequired();


            builder.Property(r => r.AttemptDateTime)
                .IsRequired();

            builder.HasOne(r => r.ApplicationUser)
                   .WithMany(ap => ap.Results) 
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Quiz)
                   .WithMany(q => q.Results) 
                   .HasForeignKey(r => r.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}