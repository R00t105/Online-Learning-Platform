using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizQuestionId { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}
