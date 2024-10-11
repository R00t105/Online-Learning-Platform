using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class QuizAnswer
    {
        public int QuizAnswerId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizQuestionId { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}
