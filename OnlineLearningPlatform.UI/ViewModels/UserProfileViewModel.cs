using OnlineLearningPlatform.DAL.Entities;

namespace OnlineLearningPlatform.UI.ViewModels
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string ? UserName  { get; set; }
        public string ? Email { get; set; }
        public DateOnly? BirhtDate { get; set; }
        public ICollection<Course>? courses { get; set; }
    }
}
