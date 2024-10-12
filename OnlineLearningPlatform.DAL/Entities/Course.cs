using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public int TrackId { get; set; }

        public virtual Track Track { get; set; }
        public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
