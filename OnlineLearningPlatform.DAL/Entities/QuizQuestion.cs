using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<QuestionAnswer> QuizAnswers { get; set; } = new List<QuestionAnswer>();
    }
}
