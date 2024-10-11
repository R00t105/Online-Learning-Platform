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
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            // Relationship
            builder.HasMany<ContentText>(c => c.ContentTexts)
                    .WithOne(ct => ct.Content)
                    .HasForeignKey(ct => ct.ContentId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
