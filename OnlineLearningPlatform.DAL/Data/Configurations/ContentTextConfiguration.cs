using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.DAL.Data.Configurations
{
    public class ContentTextConfiguration:IEntityTypeConfiguration<ContentText>
    {
        public void Configure(EntityTypeBuilder<ContentText> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Title)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            builder.Property(ct => ct.SubTitle)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            builder.Property(ct => ct.Paragraph)
                .HasColumnType("varchar(400)")
                .IsRequired(false);

            builder.HasOne<Content>(ct => ct.Content)
                    .WithMany(c => c.ContentTexts)
                    .HasForeignKey(ct => ct.ContentId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
