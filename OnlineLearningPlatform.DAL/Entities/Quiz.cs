using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MinDegree { get; set; }
        public int TotalDegree { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    }
}
