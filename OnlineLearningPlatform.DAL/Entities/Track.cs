using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
