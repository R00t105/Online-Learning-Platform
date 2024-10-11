using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? VideoUrl { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<ContentText> ContentTexts { get; set; }
    }
}
