using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform.DAL.Entities
{
    public class Enrollment
    {
        public DateTime? EnrollmentDate { get; set; }
        public string? ProgressState { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? ProgressPercentage { get; set; }
        public int ApplicationUserId { get; set; }
        public int CourseId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Course Course { get; set; }
    }
}

public enum ProgressState
{
    NotStarted,
    InProgress,
    Completed
}
