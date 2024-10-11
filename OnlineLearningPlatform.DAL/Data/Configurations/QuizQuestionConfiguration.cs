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
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.HasMany<QuizAnswer>(qq => qq.QuizAnswers)
                   .WithOne(qa => qa.QuizQuestion)
                   .HasForeignKey(qa => qa.QuizQuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
