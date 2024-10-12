using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public DateTime AttemptDateTime { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
