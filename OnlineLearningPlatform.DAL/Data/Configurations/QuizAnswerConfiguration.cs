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
    public class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
    {
        public void Configure(EntityTypeBuilder<QuizAnswer> builder)
        {
            builder.Property(qa => qa.Answer)
                   .IsRequired();
        }
    }
}
