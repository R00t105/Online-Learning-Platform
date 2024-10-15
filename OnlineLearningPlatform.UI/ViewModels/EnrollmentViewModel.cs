namespace OnlineLearningPlatform.UI.ViewModels
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? ProgressState { get; set; } // You may want to map this to an enum
        public DateTime? CompletionDate { get; set; }
        public int? ProgressPercentage { get; set; }
        public int ApplicationUserId { get; set; }
        public int CourseId { get; set; }
    }

}
