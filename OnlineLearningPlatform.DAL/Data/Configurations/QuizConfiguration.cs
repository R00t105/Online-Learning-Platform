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
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasMany<QuizQuestion>(q => q.QuizQuestions)
                    .WithOne(qq => qq.Quiz)
                    .HasForeignKey(qq => qq.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
