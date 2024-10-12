using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class ContentText
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Paragraph { get; set; }
        public int ContentId { get; set; }

        public virtual Content Content { get; set; }

    }
}
