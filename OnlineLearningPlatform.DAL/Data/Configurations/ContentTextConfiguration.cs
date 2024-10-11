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
    public class ContentTextConfiguration:IEntityTypeConfiguration<ContentText>
    {
        public void Configure(EntityTypeBuilder<ContentText> builder)
        {
            // Relationship
            builder.HasOne<Content>(ct => ct.Content)
                    .WithMany(c => c.ContentTexts)
                    .HasForeignKey(ct => ct.ContentId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
