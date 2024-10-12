using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {
            builder.HasKey(qa => qa.Id);

            builder.Property(qa => qa.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(qa => qa.Answer)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasOne<QuizQuestion>(qa => qa.QuizQuestion)
                .WithMany(qq => qq.QuizAnswers)
                .HasForeignKey(qa => qa.QuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
